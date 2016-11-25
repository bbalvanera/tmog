using System.Collections.Generic;
using System.Linq;
using TMog.Common;
using TMog.Data;
using TMog.Entities;
using TMog.Models;
using TMog.WowheadApi;

namespace TMog.Services
{
    public class ItemsService
    {
        private readonly ITMogDatabase db;
        private readonly IWowheadProvider wowProvider;

        public ItemsService(ITMogDatabase db, IWowheadProvider wowProvider)
        {
            this.db = db;
            this.wowProvider = wowProvider;
        }

        public Item GetById(int itemId)
        {
            return db.Items.Find(itemId);
        }

        public IEnumerable<Item> SearchItems(string query)
        {
            //var results = db.Items.Include(i => i.Set).Where(i => i.ItemId.ToString() == query || i.Name.StartsWith(query)).ToList();
            //var itemId  = 0;

            //if (results.Count() == 0 && int.TryParse(query, out itemId))
            //{
            //    var itemById = wowProvider.GetItemById(itemId);
            //    if (itemById != null)
            //    {
            //        Item item = this.Map(itemById);
            //        results = new List<Item>
            //        {
            //            item
            //        };
            //    }
            //}

            //return results;
            return null;
        }

        public IEnumerable<ItemsByZone> GetAllItemsByZone()
        {
            return GetItemsByZone("dbo.AllItemsByZone");
        }

        public IEnumerable<ItemsByZone> GetAllSetItemsByZone(int setId)
        {
            return GetItemsByZone("dbo.AllSetItemsByZone @p0", setId);
        }

        public IEnumerable<ItemsByZone> GetAllItemsInZone(int zoneId)
        {
            return GetItemsByZone("dbo.AllItemsInZone @p0", zoneId);
        }

        public IEnumerable<ItemsByZone> GetAllBuyableItemsByZone()
        {
            return GetItemsByZone("dbo.AllBuyableItemsByZone");
        }

        private IEnumerable<ItemsByZone> GetItemsByZone(string query, params object[] parameters)
        {
            var results = db.Execute<ZoneItem>(query, parameters);
            return ToItemsByZone(results);
        }

        private IEnumerable<ItemsByZone> ToItemsByZone(IEnumerable<ZoneItem> items)
        {
            return items.GroupBy(z => z.ZoneId).Select(z => new ItemsByZone(z.Key, z));
        }

        private Item Map(IWowheadItem wowHeadItem)
        {
            return new Item
            {
                ItemId        = wowHeadItem.Id,
                Name          = wowHeadItem.Name,
                Slot          = this.SlotMapper.Map(wowHeadItem.Slot),
                Quality       = (QualityType)wowHeadItem.Quality.Value,
                Class         = wowHeadItem.Class,
                Subclass      = wowHeadItem.Subclass,
                iLevel        = wowHeadItem.iLevel,
                RequiredLevel = wowHeadItem.RequiredLevel,
                DisplayId     = wowHeadItem.DisplayId,
                Flags         = wowHeadItem.Flags,
                BuyPrice      = wowHeadItem.BuyPrice,
                SellPrice     = wowHeadItem.SellPrice,
                Acquired      = false
            };
        }

        private PairMapper<SlotType, int> SlotMapper = new PairMapper<SlotType, int>(new Pair<SlotType, int>[]
        {
            new Pair<SlotType, int>(SlotType.Other, 0),
            new Pair<SlotType, int>(SlotType.Head, 1),
            new Pair<SlotType, int>(SlotType.Necklace, 2),
            new Pair<SlotType, int>(SlotType.Shoulder, 3),
            new Pair<SlotType, int>(SlotType.Shirt, 4),
            new Pair<SlotType, int>(SlotType.Chest, 5),
            new Pair<SlotType, int>(SlotType.Waist, 6),
            new Pair<SlotType, int>(SlotType.Legs, 7),
            new Pair<SlotType, int>(SlotType.Feet, 8),
            new Pair<SlotType, int>(SlotType.Wrist, 9),
            new Pair<SlotType, int>(SlotType.Hands, 10)
        });
    }
}
