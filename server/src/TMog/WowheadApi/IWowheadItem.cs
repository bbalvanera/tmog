using System.Collections.Generic;

namespace TMog.WowheadApi
{
    public interface IWowheadItem
    {
        int Id { get; }

        string Name { get; }

        int Slot { get; }

        int? Class { get; }

        int? Subclass { get; }

        int? DisplayId { get; }

        int? Flags { get; }

        int? iLevel { get; }

        int? RequiredLevel { get; }

        int? Quality { get; }

        long? BuyPrice { get; }

        long? SellPrice { get; }

        IEnumerable<IWowheadItemSource> Sources { get; }
    }
}
