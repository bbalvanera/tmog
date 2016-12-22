using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Source> Sources { get; set; }
    }
}
