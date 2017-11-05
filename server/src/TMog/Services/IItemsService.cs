using System.Collections.Generic;
using System.Threading.Tasks;
using TMog.Entities;
using TMog.Entities.Views;

namespace TMog.Services
{
    public interface IItemsService
    {
        Item GetById(int itemId);

        Task<IEnumerable<Item>> SearchItems(string query);

        Task<IEnumerable<ZonesByRegion>> GetAllItemsByRegion();

        Task<IEnumerable<ZonesByRegion>> GetAllSetItemsByZone(int setId);

        Task<IEnumerable<ZonesByRegion>> GetAllItemsInZone(int zoneId);

        Task<IEnumerable<ZonesByRegion>> GetAllBuyableItemsByZone();
    }
}