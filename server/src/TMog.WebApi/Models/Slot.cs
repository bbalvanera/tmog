using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class Slot
    {
        public int SlotNumber { get; set; }

        public string SlotName { get; set; }

        public bool Complete { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
