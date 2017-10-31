using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace TMog.WebApi.Controllers
{
    public class DefaultController : ApiController
    {
        public string Get()
        {
            var queryString = Request.GetQueryNameValuePairs();
            var parameters = new StringBuilder();

            foreach (var item in queryString)
            {
                parameters.Append($"Key: {item.Key} Value: {item.Value} | ");
            }

            return $"{DateTime.Now.ToString("F")}. Up and running on {Environment.OSVersion.ToString()}. Parameters: {parameters}";
        }
    }
}
