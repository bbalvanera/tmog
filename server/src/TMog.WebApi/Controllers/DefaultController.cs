using System;
using System.Configuration;
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

            var output = new StringBuilder();

            output.Append(DateTime.Now.ToString("F"));
            output.Append($". Up and running on {Environment.OSVersion.ToString()} - {ConfigurationManager.AppSettings["Env"]}. ");

            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                output.Append($"Connection string #{i}: {ConfigurationManager.ConnectionStrings[i].ConnectionString}. ");
            }
            
            output.Append($"Parameters: {parameters}");

            return output.ToString();
        }
    }
}
