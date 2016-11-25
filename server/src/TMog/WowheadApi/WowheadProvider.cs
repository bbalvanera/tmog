using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using TMog.WowheadApi.Infrastructure;

namespace TMog.WowheadApi
{
    public class WowheadProvider : IWowheadProvider
    {
        private const string WOWHEAD_ITEM_URI_TEMPLATE = "http://www.wowhead.com/item={0}&xml";
        private const string WOWHEAD_SET_URI_TEMPLATE = "http://www.wowhead.com/transmog-set={0}&xml";
        private const string SET_NAME_XPATH = "//div[@class='text']/h1[@class='heading-size-1']";
        private const string SET_NOTFOUND_XPATH = "//div[@id='inputbox-error']";
        private const string ITEM_ID_XPATH = "//div[@id='transmog']//li/a[@href]";

        public async Task<IWowheadItem> GetItemById(int itemId)
        {
            if (itemId <= 0)
                return null;

            XDocument data = await RetrieveItemXmlData(itemId);

            if (ItemNotFound(data))
                return null;

            return TranslateData(data);
        }

        public async Task<IWowheadSet> GetSetById(int setId)
        {
            var setData = await RetrieveSetHtmlData(setId);

            if (setData == null || SetNotFound(setData))
            {
                return null;
            }

            var wowHeadSet = await TranslateData(setData);
            wowHeadSet.WowheadSetId = setId;

            return wowHeadSet;
        }

        private async Task<XDocument> RetrieveItemXmlData(int itemId)
        {
            try
            {
                var loader = new Task<XDocument>(() => XDocument.Load($"http://www.wowhead.com/item={itemId}&xml"));
                loader.Start();

                return await loader;
            }
            catch (Exception)
            {
                throw new WowheadProviderException("Unable to retrieve information from wowhead.com");
            }
        }

        private async Task<HtmlNode> RetrieveSetHtmlData(int setId)
        {
            try
            {
                var loader = new Task<HtmlNode>(() => {
                    var htmlWeb = new HtmlWeb();
                    var htmlDoc = htmlWeb.Load($"http://www.wowhead.com/transmog-set={setId}&xml");

                    return htmlDoc.DocumentNode;
                });

                loader.Start();

                return await loader;
            }
            catch (Exception)
            {
                throw new WowheadProviderException("Unable to retrieve set info from wowhead");
            }
        }

        private bool ItemNotFound(XDocument data)
        {
            return data.Descendants("error").FirstOrDefault() != null;
        }

        private bool SetNotFound(HtmlNode setData)
        {
            return setData.SelectSingleNode(SET_NOTFOUND_XPATH) != null;
        }

        private IWowheadItem TranslateData(XDocument data)
        {
            WowheadItem wowHeadItem = null;

            var jsonData      = GetJsonData(data);
            var jsonEquipData = GetJsonEquipData(data);
            var itemXmlData   = GetItemXmlData(data);

            if (jsonData != null)
            {
                wowHeadItem = new WowheadItem
                {
                    Id            = jsonData.Id,
                    Name          = (char.IsDigit(jsonData.Name[0]) ? jsonData.Name.Substring(1) : jsonData.Name),
                    Slot          = jsonData.Slot,
                    Quality       = itemXmlData.Quality,
                    Class         = itemXmlData.Class,
                    Subclass      = itemXmlData.Subclass,
                    Flags         = jsonData.Flags,
                    iLevel        = jsonData.iLvl,
                    RequiredLevel = jsonData.RequiredLevel,
                    DisplayId     = jsonData.DisplayId,
                    Sources       = this.GetSources(jsonData.Sources, jsonData.SourceDetails)
                };

                if (jsonEquipData != null)
                {
                    wowHeadItem.SellPrice = jsonEquipData.SellPrice;
                    wowHeadItem.BuyPrice  = jsonEquipData.BuyPrice;
                }
            }

            return wowHeadItem;
        }

        private async Task<WowheadSet> TranslateData(HtmlNode data)
        {
            var wowHeadSet = new WowheadSet();
            var itemIds    = GetItemIds(data);

            wowHeadSet.Name = GetSetName(data);

            if (itemIds.Any<int>())
            {
                var list = new List<IWowheadItem>();
                foreach (int itemId in itemIds)
                {
                    var itemById = await GetItemById(itemId);
                    if (itemById != null)
                    {
                        list.Add(itemById);
                    }
                }
                wowHeadSet.Items = list;
            }
            return wowHeadSet;
        }

        private string GetSetName(HtmlNode data)
        {
            var setNameNode = data.SelectSingleNode(SET_NAME_XPATH);

            return setNameNode?.InnerText;
        }

        private IEnumerable<int> GetItemIds(HtmlNode data)
        {
            var result = new List<int>();
            var itemsCollection = data.SelectNodes(ITEM_ID_XPATH);

            if (itemsCollection != null)
            {
                int itemId = 0;
                var items = itemsCollection.Select(i => i.Attributes["href"].Value)
                                           .Where(s => s.StartsWith("/item="))
                                           .Select(s => Regex.Match(s, "\\d+").Value);

                var list = new List<int>();
                foreach (var id in items)
                {
                    if (int.TryParse(id, out itemId))
                    {
                        list.Add(itemId);
                    }
                }

                result = list;
            }

            return result;
        }

        private WowheadJsonData GetJsonData(XDocument data)
        {
            WowheadJsonData result = null;
            var json  = data.Descendants("json").FirstOrDefault();
            var cData = json.DescendantNodes().FirstOrDefault(node => node.NodeType == XmlNodeType.CDATA);

            if (cData != null)
            {
                var jsonItem = $"{{{cData.Parent.Value.Trim()}}}";
                result = JsonConvert.DeserializeObject<WowheadJsonData>(jsonItem);
            }

            return result;
        }

        private WowheadJsonEquipData GetJsonEquipData(XDocument data)
        {
            WowheadJsonEquipData result = null;
            var jsonEquip = data.Descendants("jsonEquip").FirstOrDefault();

            if (jsonEquip != null)
            {
                var cData = jsonEquip.DescendantNodes().FirstOrDefault((XNode node) => node.NodeType == XmlNodeType.CDATA);
                if (cData != null)
                {
                    var text = $"{{{cData.Parent.Value.Trim()}}}";
                    result = JsonConvert.DeserializeObject<WowheadJsonEquipData>(text);
                }
            }

            return result;
        }

        private ItemXmlData GetItemXmlData(XDocument data)
        {
            var itemXmlData = new ItemXmlData();
            var value = 0;
            var descendants = new string[]
            {
                "quality",
                "class",
                "subclass"
            };

            for (int i = 0; i < descendants.Length; i++)
            {
                var xElement = data.Descendants(descendants[i]).FirstOrDefault();
                if (xElement != null)
                {
                    if (int.TryParse(xElement.Attribute("id").Value, out value))
                    {
                        itemXmlData[i] = new int?(value);
                    }
                }
            }

            return itemXmlData;
        }

        private IEnumerable<IWowheadItemSource> GetSources(int[] sources, WowheadJsonSource[] details)
        {
            var list = new List<IWowheadItemSource>();

            if (sources != null && sources.Length > 0)
            {
                for (int i = 0; i < sources.Length; i++)
                {
                    var value = sources[i];
                    var wowheadJsonSource = new WowheadJsonSource();

                    if (details != null && details.Length > i)
                    {
                        wowheadJsonSource = details[i];
                    }

                    list.Add(new WowheadItemSource
                    {
                        Type      = value,
                        SubType   = wowheadJsonSource.Type,
                        Name      = wowheadJsonSource.Name,
                        WowheadId = wowheadJsonSource.Id,
                        DropLevel = wowheadJsonSource.DropLevel,
                        Zone      = wowheadJsonSource.Zone
                    });
                }
            }

            return list;
        }
    }
}
