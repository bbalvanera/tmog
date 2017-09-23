using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMog.Data;
using TMog.Entities;
using TMog.Entities.Views;
using TMog.WowApi;

namespace TMog.Services
{
    public class ZonesService : IZonesService
    {
        private readonly TMogDatabase tmogContext;
        private readonly IWowProvider wowProvider;

        public ZonesService(TMogDatabase tmogContext, IWowProvider wowProvider)
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

        public async Task<ICollection<ItemByLocation>> GetAllItemsByLocation(int? locationId = null)
        {
            var items = tmogContext.Database.SqlQuery<ItemByLocation>("dbo.AllItemsByLocation");

            return await items.ToListAsync();
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

                tmogContext.Zones.Add(zone);
            }

            tmogContext.SaveChanges();
        }

        private Location GetOrCreateLocation(IWowLocation location)
        {
            if (location == null)
            {
                return null;
            }

            return tmogContext.Locations.Find(location.Id) ?? tmogContext.Locations.Add(Mapper.Map<Location>(location));
        }
    }
}
