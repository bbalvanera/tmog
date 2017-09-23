using System.Collections.Generic;
using System.Threading.Tasks;
using TMog.Entities;
using TMog.Entities.Views;

namespace TMog.Services
{
    public interface IZonesService
    {
        Task<Zone> GetOrCreateZone(int? zoneId);

        Task<ICollection<ItemByLocation>> GetAllItemsByLocation(int? locationId = null);

        Task LoadZonesFromWowApi();
    }
}
