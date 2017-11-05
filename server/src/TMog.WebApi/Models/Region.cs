using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ZoneItems> Zones { get; set; }
    }
}
