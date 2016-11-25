using Newtonsoft.Json;

namespace TMog.WowheadApi.Infrastructure
{
    internal class WowheadJsonSource
    {
        [JsonProperty("ti")]
        public int? Id
        {
            get;
            set;
        }

        [JsonProperty("t")]
        public int? Type
        {
            get;
            set;
        }

        [JsonProperty("n")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("dd")]
        public int? DropLevel
        {
            get;
            set;
        }

        [JsonProperty("z")]
        public int? Zone
        {
            get;
            set;
        }
    }
}
