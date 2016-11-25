using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TMog.Business;
using TMog.Services;
using TMog.WebApi.Models;

namespace TMog.WebApi.Controllers
{
    [RoutePrefix("tmog-sets")]
    public class TMogSetsController : ApiController
    {
        private readonly ISetsService service;

        public TMogSetsController(ISetsService service)
        {
            this.service = service;
        }

        [Route("")]
        public async Task<IEnumerable<TMogSet>> Get()
        {
            var results = await service.GetAll();
            return results.Select(set => 
            {
                var tset = new TMogSet();
                Map(set, tset);

                return tset;
            });
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var tmogSet = new TMogSet();
            var set     = await service.GetById(id);

            if (set != null)
            {
                Map(set, tmogSet);

                return Ok(tmogSet);
            }

            return NotFound();
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Post(int id)
        {
            var set = await service.GetById(id);

            return CreatedAtRoute("tmog-sets", id, set);
        }

        private void Map(Entities.Set source, TMogSet target)
        {
            var slots = new SlotManager(source.Slots);

            target.Id             = source.SetId;
            target.Name           = source.Name;
            target.TotalSlots     = slots.ActiveSlotCount;
            target.CompletedSlots = slots.CompletedSlotCount;
        }
    }
}
