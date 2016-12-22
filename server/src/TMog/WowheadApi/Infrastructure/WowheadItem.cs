using System.Collections.Generic;

namespace TMog.WowheadApi.Infrastructure
{
    internal class WowheadItem : IWowheadItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Slot { get; set; }

        public int? Class { get; set; }

        public int? Subclass { get; set; }

        public int? DisplayId { get; set; }

        public int? Flags { get; set; }

        public int? iLevel { get; set; }

        public int? RequiredLevel { get; set; }

        public int? Quality { get; set; }

        public long? BuyPrice { get; set; }

        public long? SellPrice { get; set; }

        public IEnumerable<IWowheadItemSource> Sources { get; set; }
    }
}
