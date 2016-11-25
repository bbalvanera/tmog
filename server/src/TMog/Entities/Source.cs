using System.Collections.Generic;

namespace TMog.Entities
{
    public class Source
    {
        public int SourceId
        {
            get;
            set;
        }

        public SourceType Type
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public SourceSubType? SubType
        {
            get;
            set;
        }

        public DropLevel? DropLevel
        {
            get;
            set;
        }

        public int? WowheadId
        {
            get;
            set;
        }

        public int? ZoneId
        {
            get;
            set;
        }

        public Zone Zone
        {
            get;
            set;
        }

        public ICollection<Item> Items
        {
            get;
            set;
        }
    }
}
