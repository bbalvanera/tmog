using TMog.Entities;

namespace TMog.Models
{
    public class ZoneItem
    {
        public int? ZoneId
        {
            get;
            set;
        }

        public string ZoneName
        {
            get;
            set;
        }

        public int ItemId
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public SlotType ItemSlot
        {
            get;
            set;
        }

        public QualityType ItemQuality
        {
            get;
            set;
        }

        public bool ItemAcquired
        {
            get;
            set;
        }

        public bool Buyable
        {
            get;
            set;
        }

        public int SetId
        {
            get;
            set;
        }

        public string SetName
        {
            get;
            set;
        }

        public int? WowheadId
        {
            get;
            set;
        }

        public SourceType? SourceType
        {
            get;
            set;
        }

        public SourceSubType? SourceSubType
        {
            get;
            set;
        }

        public string SourceName
        {
            get;
            set;
        }
    }
}
