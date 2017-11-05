namespace TMog.WebApi.Models
{
    public class ZoneItem
    {
        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public int ZoneId { get; set; }

        public string ZoneName { get; set; }

        public int ZoneDifficulty { get; set; }

        public int SetId { get; set; }

        public string SetName { get; set; }

        public string SetSlots { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int ItemQuality { get; set; }

        public string Slot { get; set; }

        public int? SourceId { get; set; }

        public string Source { get; set; }

        public string SourceType { get; set; }

        public string SourceSubType { get; set; }
    }
}
