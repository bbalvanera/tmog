using System.Collections.Generic;

namespace TMog.WowheadApi
{
    public interface IWowheadSet
    {
        int WowheadSetId { get; }

        string Name { get; }

        IEnumerable<IWowheadItem> Items { get; }
    }
}
