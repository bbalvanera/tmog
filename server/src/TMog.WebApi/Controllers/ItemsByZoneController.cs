using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TMog.Models;
using TMog.Services;

namespace TMog.WebApi.Controllers
{
    public class ItemsByZoneController : ApiController
    {
        private readonly IItemsService service;

        public ItemsByZoneController(IItemsService service)
        {
            this.service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var queryString = Request.GetQueryNameValuePairs().FirstOrDefault();
            Task<IEnumerable<ItemsByZone>> content;
            
            switch (queryString.Key)
            {
                default:
                    content = service.GetAllItemsByZone();
                    break;
            }

            return Ok(new { items = await content });
        }
    }
}
