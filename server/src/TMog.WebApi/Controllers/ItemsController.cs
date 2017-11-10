using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TMog.Services;
using TMog.WebApi.Models;

namespace TMog.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemsService service;

        public ItemsController(IItemsService service)
        {
            this.service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            Task<IEnumerable<Entities.Views.ZonesByRegion>> results;

            var  queryString = Request.GetQueryNameValuePairs().FirstOrDefault();
            var  filter      = queryString.Key ?? "";
            int? arg       = null;

            if (queryString.Key != null)
            {
                // if a key is present a value must also exist
                if (!int.TryParse(queryString.Value, out int result))
                {
                    return BadRequest("Invalid parameter");
                }

                arg = result;
            }

            switch (filter.ToLowerInvariant())
            {
                case "setid":
                    results = service.GetAllItemsBySet(arg.Value);
                    break;
                case "regionid":
                    results = service.GetAllItemsByRegion(arg);
                    break;
                case "zoneid":
                    results = service.GetAllItemsByZone(arg.Value);
                    break;
                case "":
                    results = service.GetAllItemsByRegion();
                    break;
                default:
                    return BadRequest("Invalid parameter");
            }

            var mapped = Mapper.Map<IEnumerable<Region>>(await results);
            return Ok(new { Regions = mapped });
        }
    }
}
