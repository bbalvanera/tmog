using System.Collections.Generic;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services.Infrastructure.WowheadSetMapProfileTests
{
    public class WowheadSet : IWowheadSet
    {
        public int WowheadSetId { get; set; }

        public string Name { get; set; }

        public IEnumerable<IWowheadItem> Items { get; set; }
    }
}
