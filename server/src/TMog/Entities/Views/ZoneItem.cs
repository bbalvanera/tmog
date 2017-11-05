namespace TMog.Entities.Views
{
    public class ZoneItem
    {
        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public int ZoneId { get; set; }

        public string ZoneName { get; set; }

        public DropLevel ZoneDifficulty { get; set; }

        public int SetId { get; set; }

        public string SetName { get; set; }

        public string SetSlots { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public QualityType ItemQuality { get; set; }

        public SlotType Slot { get; set; }

        public int? SourceId { get; set; }

        public string Source { get; set; }

        public SourceType? SourceType { get; set; }

        public SourceSubType? SourceSubType { get; set; }
    }
}
