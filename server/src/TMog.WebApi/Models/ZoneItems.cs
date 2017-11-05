using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class ZoneItems
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Difficulty { get; set; }

        public IEnumerable<ZoneItem> Items { get; set; }
    }
}
