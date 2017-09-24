using System;
using System.Web.Http;

namespace TMog.WebApi.Controllers
{
    public class DefaultController : ApiController
    {
        public string Get()
        {
            return $"{DateTime.Now.ToString("F")}. Up and running on {Environment.OSVersion.ToString()}";
        }
    }
}
