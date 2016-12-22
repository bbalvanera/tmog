using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Source> Sources { get; set; }
    }
}
