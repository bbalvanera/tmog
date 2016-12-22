using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMog.Data;
using TMog.Entities;
using TMog.WowApi;

namespace TMog.Services
{
    public class ZonesService : IZonesService
    {
        private readonly ITMogDatabase db;
        private readonly IWowProvider wowProvider;

        public ZonesService(ITMogDatabase db, IWowProvider wowProvider)
        {
            this.db = db;
            this.wowProvider = wowProvider;
        }

        public async Task<Zone> GetOrCreateZone(int? zoneId)
        {
            Zone result = null;
            if (zoneId.HasValue)
            {
                result = db.Zones.Find(zoneId.Value) ?? db.Zones.Add(new Zone { ZoneId = zoneId });
            }

            return await Task.FromResult(result);
        }

        public async Task LoadZonesFromWowApi()
        {
            var wowZones = await wowProvider.GetAllZones();

            if (wowZones == null || !wowZones.Any())
            {
                return;
            }

            foreach (var wowZone in wowZones)
            {
                var zone = Mapper.Map<Zone>(wowZone);
                zone.Location = GetOrCreateLocation(wowZone.Location);

                db.Zones.Add(zone);
            }

            db.SaveChanges();
        }

        private Location GetOrCreateLocation(IWowLocation location)
        {
            if (location == null)
            {
                return null;
            }

            return db.Locations.Find(location.Id) ?? db.Locations.Add(Mapper.Map<Location>(location));
        }
    }
}
