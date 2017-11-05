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
    public class ItemsByRegionController : ApiController
    {
        private readonly IItemsService service;

        public ItemsByRegionController(IItemsService service)
        {
            this.service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var queryString = Request.GetQueryNameValuePairs().FirstOrDefault();
            Task<IEnumerable<Entities.Views.ZonesByRegion>> results;

            switch (queryString.Key)
            {
                default:
                    results = service.GetAllItemsByRegion();
                    break;
            }

            var mapped = Mapper.Map<IEnumerable<Region>>(await results);
            return Ok(new { Regions = mapped });
        }
    }
}
