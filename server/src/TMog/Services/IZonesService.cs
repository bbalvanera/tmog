using System.Threading.Tasks;
using TMog.Entities;

namespace TMog.Services
{
    public interface IZonesService
    {
        Task<Zone> GetOrCreateZone(int? zoneId);
    }
}
