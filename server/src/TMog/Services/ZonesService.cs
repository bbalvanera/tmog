using System.Threading.Tasks;
using TMog.Data;
using TMog.Entities;
using TMog.WowApi;

namespace TMog.Services
{
    public class ZonesService : IZonesService
    {
        private readonly ITMogDatabase tmogContext;
        private readonly IWowProvider wowProvider;

        public ZonesService(ITMogDatabase tmogContext, IWowProvider wowProvider)
        {
            this.tmogContext = tmogContext;
            this.wowProvider = wowProvider;
        }

        public async Task<Zone> GetOrCreateZone(int? zoneId)
        {
            Zone result = null;
            if (zoneId.HasValue)
            {
                result = tmogContext.Zones.Find(zoneId.Value) ?? tmogContext.Zones.Add(new Zone { ZoneId = zoneId });
            }

            return await Task.FromResult(result);
        }
    }
}
