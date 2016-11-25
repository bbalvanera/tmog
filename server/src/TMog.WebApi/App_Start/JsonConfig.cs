using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace TMog.WebApi
{
    public static class JsonConfig
    {
        public static void Register(JsonMediaTypeFormatter jsonFormatter)
        {
            var settings = jsonFormatter.SerializerSettings;
            
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
