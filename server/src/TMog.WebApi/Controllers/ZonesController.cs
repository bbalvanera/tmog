using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
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

        [HttpPost]
        public async Task<IHttpActionResult> Load()
        {
            await zonesService.LoadZonesFromWowApi();

            return new StatusCodeResult((HttpStatusCode)204, this);
        }
    }
}
