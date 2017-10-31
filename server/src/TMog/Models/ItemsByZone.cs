using System.Collections.Generic;

namespace TMog.Models
{
    public class ItemsByZone
    {
        public ItemsByZone(string continent, IEnumerable<ZoneItem> items)
        {
            Continent = continent;
            Items  = items;
        }

        public string Continent { get; set; }

        public IEnumerable<ZoneItem> Items { get; set; }
    }
}
