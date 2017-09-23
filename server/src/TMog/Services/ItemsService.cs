using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMog.Data;
using TMog.Entities;
using TMog.Models;
using TMog.WowheadApi;

namespace TMog.Services
{
    public class ItemsService : IItemsService
    {
        private readonly ITMogDatabase tmogContext;
        private readonly IWowheadProvider wowheadProvider;

        public ItemsService(ITMogDatabase tmogContext, IWowheadProvider wowheadProvider)
        {
            this.tmogContext = tmogContext;
            this.wowheadProvider = wowheadProvider;
        }

        public Item GetById(int itemId)
        {
            return tmogContext.Items.Find(itemId);
        }

        public async Task<IEnumerable<Item>> SearchItems(string query)
        {
            var results = await tmogContext.Items
                .Include(i => i.Sets)
                .Where(i => i.ItemId.ToString() == query || i.Name.Contains(query)).ToListAsync();

            var itemId = 0;
            if (results.Count() == 0 && int.TryParse(query, out itemId))
            {
                var wowheadItem = await wowheadProvider.GetItemById(itemId);
                if (wowheadItem != null)
                {
                    Item item = Mapper.Map<Item>(wowheadItem);
                    results = new List<Item>
                    {
                        item
                    };
                }
            }

            return results;
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
            var results = tmogContext.Execute<ZoneItem>(query, parameters);
            return ToItemsByZone(results);
        }

        private IEnumerable<ItemsByZone> ToItemsByZone(IEnumerable<ZoneItem> items)
        {
            return items.GroupBy(z => z.ZoneId).Select(z => new ItemsByZone(z.Key, z));
        }
    }
}
