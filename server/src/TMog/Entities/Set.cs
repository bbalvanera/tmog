using System.Collections.Generic;
using TMog.Business;

namespace TMog.Entities
{
    public class Set
    {
        public int SetId { get; set; }

        public string Name { get; set; }

        public string Slots { get; set; }

        public ICollection<Item> Items
        {
            get;
            set;
        }
    }
}
