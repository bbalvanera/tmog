using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class TmogSet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TotalSlots { get; set; }

        public int CompletedSlots { get; set; }

        public IEnumerable<Slot> Slots { get; set; }
    }
}
