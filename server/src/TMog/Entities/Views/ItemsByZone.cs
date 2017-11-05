using System.Collections.Generic;

namespace TMog.Entities.Views
{
    public class ItemsByZone
    {
        public int ZoneId { get; set; }
        
        public string ZoneName { get; set; }

        public DropLevel ZoneDifficulty { get; set; }

        public IEnumerable<ZoneItem> Items { get; set; }
    }
}
