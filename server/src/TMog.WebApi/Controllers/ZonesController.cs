using System.Web.Http;
using TMog.Services;

namespace TMog.WebApi.Controllers
{
    public class ZonesController : ApiController
    {
        private readonly IZonesService zonesService;

        public ZonesController(IZonesService zonesService)
        {
            this.zonesService = zonesService;
        }

        
    }
}
