using System.Collections.Generic;
using System.Threading.Tasks;
using TMog.Entities;
using TMog.Models;

namespace TMog.Services
{
    public interface IItemsService
    {
        Item GetById(int itemId);

        Task<IEnumerable<Item>> SearchItems(string query);

        Task<IEnumerable<ItemsByZone>> GetAllItemsByZone();

        Task<IEnumerable<ItemsByZone>> GetAllSetItemsByZone(int setId);

        Task<IEnumerable<ItemsByZone>> GetAllItemsInZone(int zoneId);

        Task<IEnumerable<ItemsByZone>> GetAllBuyableItemsByZone();
    }
}