using System.Web.Http;

namespace TMog.WebApi.Controllers
{
  public class DefaultController : ApiController
  {
    public string Get()
    {
      return "Hello world from default controller";
    }
  }
}
