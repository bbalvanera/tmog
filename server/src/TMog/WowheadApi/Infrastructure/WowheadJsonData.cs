using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TMog.WowheadApi.Infrastructure
{
    internal class WowheadJsonData
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        [JsonProperty("displayid")]
        public int? DisplayId
        {
            get;
            set;
        }

        [JsonProperty("flags2")]
        public int? Flags
        {
            get;
            set;
        }

        [JsonProperty("reqlevel")]
        public int? RequiredLevel
        {
            get;
            set;
        }

        public int? Class
        {
            get;
            set;
        }

        public int? Subclass
        {
            get;
            set;
        }

        public int Slot
        {
            get;
            set;
        }

        [JsonProperty("level")]
        public int? iLvl
        {
            get;
            set;
        }

        [JsonProperty("source")]
        public int[] Sources
        {
            get;
            set;
        }

        [JsonProperty("sourcemore")]
        public WowheadJsonSource[] SourceDetails
        {
            get;
            set;
        }
    }
}
