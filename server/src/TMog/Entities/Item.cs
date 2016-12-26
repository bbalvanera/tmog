using System.Collections.Generic;

namespace TMog.Entities
{
    public class Item
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public SlotType Slot { get; set; }

        public int? Class { get; set; }

        public int? Subclass { get; set; }

        public int? iLevel { get; set; }

        public int? RequiredLevel { get; set; }

        public int? DisplayId { get; set; }

        public int? Flags { get; set; }

        public QualityType Quality { get; set; }

        public long? BuyPrice { get; set; }

        public long? SellPrice { get; set; }

        public Source Source { get; set; }
    }
}
