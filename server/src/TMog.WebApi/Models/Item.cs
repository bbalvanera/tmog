using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Quality { get; set; }

        public Source Source { get; set; }

        public IEnumerable<TMogSet> Sets { get; set; }
    }
}
