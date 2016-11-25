using System.Collections.Generic;

namespace TMog.Models
{
    public class ItemsByZone
    {
        public ItemsByZone(int? zoneId, IEnumerable<ZoneItem> items)
        {
            this.ZoneId = ZoneId;
            this.Items  = items;
        }

        public int? ZoneId { get; set; }

        public IEnumerable<ZoneItem> Items { get; set; }
    }
}
