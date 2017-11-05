using System.Collections.Generic;

namespace TMog.Entities.Views
{
    public class ZonesByRegion
    {
        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public IEnumerable<ItemsByZone> Zones { get; set; }
    }
}
