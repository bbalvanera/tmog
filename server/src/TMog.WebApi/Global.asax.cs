using System.Web.Http;

namespace TMog.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            JsonConfig.Register(GlobalConfiguration.Configuration.Formatters.JsonFormatter);
            AutoMapperConfig.Configure();
        }
    }
}
