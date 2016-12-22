using System.Collections.Generic;

namespace TMog.WowheadApi.Infrastructure
{
    internal class WowheadSet : IWowheadSet
    {
        public int WowheadSetId { get; set; }

        public string Name { get; set; }

        public IEnumerable<IWowheadItem> Items { get; set; }
    }
}
