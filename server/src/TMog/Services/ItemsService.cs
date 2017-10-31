using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public Item GetById(int itemId) => tmogContext.Items.Find(itemId);

        public async Task<IEnumerable<Item>> SearchItems(string query)
        {
            var results = await tmogContext.Items
                .Include(i => i.Sets)
                .Where(i => i.ItemId.ToString() == query || i.Name.Contains(query)).ToListAsync();

            if (results.Count() == 0 && int.TryParse(query, out int itemId))
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

        public async Task<IEnumerable<ItemsByZone>> GetAllItemsByZone() => await Execute("dbo.AllItemsByZone");

        public async Task<IEnumerable<ItemsByZone>> GetAllSetItemsByZone(int setId) => await Execute("dbo.AllSetItemsByZone @p0", setId);

        public async Task<IEnumerable<ItemsByZone>> GetAllItemsInZone(int zoneId) => await Execute("dbo.AllItemsInZone @p0", zoneId);

        public async Task<IEnumerable<ItemsByZone>> GetAllBuyableItemsByZone() => await Execute("dbo.AllBuyableItemsByZone");

        private async Task<IEnumerable<ItemsByZone>> Execute(string query, params object[] parameters)
        {
            var results = tmogContext.Execute<ZoneItem>(query, parameters);
            return ToItemsByZone(await results);
        }

        private IEnumerable<ItemsByZone> ToItemsByZone(IEnumerable<ZoneItem> items) => items.GroupBy(z => z.Continent).Select(z => new ItemsByZone(z.Key, z));
    }
}
