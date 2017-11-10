using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TMog.Data;
using TMog.Entities;
using TMog.Entities.Views;
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

        public async Task<IEnumerable<ZonesByRegion>> GetAllItemsBySet(int setId) => await Execute("dbo.AllItems @p0, @p1, @p2", setId, null, null);

        public async Task<IEnumerable<ZonesByRegion>> GetAllItemsByRegion(int? regionId = null) => await Execute("dbo.AllItems @p0, @p1, @p2", null, regionId, null);

        public async Task<IEnumerable<ZonesByRegion>> GetAllItemsByZone(int zoneId) => await Execute("dbo.AllItems @p0, @p1, @p2", null, null, zoneId);

        public async Task<IEnumerable<ZonesByRegion>> GetAllBuyableItemsByZone() => await Execute("dbo.AllBuyableItemsByZone");

        private async Task<IEnumerable<ZonesByRegion>> Execute(string query, params object[] parameters)
        {
            var results = tmogContext.Execute<ZoneItem>(query, parameters);
            return ToZonesByRegion(await results);
        }

        private IEnumerable<ZonesByRegion> ToZonesByRegion(IEnumerable<ZoneItem> allItems)
        {
            // group by ZoneName and ZoneDifficulty and then group by Region
            // this grouping returns:
            // Region 1
            // -- Zone 1 & Difficulty 1
            // ---- Item 1
            // ---- Item 2
            // ---- Item 3
            // Region 2
            // -- Zone 1 & Difficulty 2
            // ---- Item 1
            // ---- Item 2
            // ---- Item 3
            // Region 2
            // -- Zone 2 & Difficulty 1
            // ---- Item 1
            // ---- Item 2
            // ---- Item 3
            // Region 2
            // -- Zone 2 & Difficulty 2
            // ---- Item 1
            // ---- Item 2
            // ---- Item 3
            var allZonesByRegion = 
                allItems
                    .GroupBy(item1 => item1.RegionId)
                    .Select(zonesByRegion => new ZonesByRegion
                    {
                        RegionId   = zonesByRegion.Key,
                        RegionName = zonesByRegion.First().RegionName,
                        Zones         = zonesByRegion
                                            .GroupBy(itemsByZone => itemsByZone.ZoneName + itemsByZone.ZoneDifficulty) // by zone & difficulty e.g. Ulduar25Heroic or Ulduar10Heroic
                                            .Select(itemsByZone => new ItemsByZone
                                            {
                                                ZoneId         = itemsByZone.First().ZoneId,
                                                ZoneName       = itemsByZone.First().ZoneName,
                                                ZoneDifficulty = itemsByZone.First().ZoneDifficulty,
                                                Items          = itemsByZone
                                            })
                    });

            return allZonesByRegion;
        }
    }
}
