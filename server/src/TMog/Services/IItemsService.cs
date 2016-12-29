using System.Collections.Generic;
using System.Threading.Tasks;
using TMog.Entities;

namespace TMog.Services
{
    public interface IItemsService
    {
        Item GetById(int itemId);
        Task<IEnumerable<Item>> SearchItems(string query);
    }
}