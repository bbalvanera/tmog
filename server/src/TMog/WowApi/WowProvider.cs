using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMog.WowApi.Infrastructure;

namespace TMog.WowApi
{
    public class WowProvider : IWowProvider
    {
        public async Task<IEnumerable<IWowZone>> GetAllZones()
        {
            var allZonesJson = await GetJsonFromWowApi();
            try
            {
                var allZones = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<WowZones>(allZonesJson));
                return allZones.Zones;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetJsonFromWowApi()
        {
            try
            {
                var request = WebRequest.CreateHttp("https://us.api.battle.net/wow/zone/?locale=en_US&apikey=ygz6ba6wfk2ux7uesk9p7rkfqza4t4cn");
                var response = await request.GetResponseAsync();

                return ReadResponse(response);
            }
            catch (WebException webex)
            {
                var response = ReadResponse(webex.Response);
                throw new WowProviderException(response);
            }
        }

        private string ReadResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
