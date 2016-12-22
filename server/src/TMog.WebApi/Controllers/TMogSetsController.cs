﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
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
            return Mapper.Map<IEnumerable<TMogSet>>(results);
        }

        [Route("{id:int}", Name = "tmog-sets")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var set = await service.GetById(id);
            if (set != null)
            {
                var slotMan  = new SlotManager(set.Slots);
                var tmogSet  = Mapper.Map<TMogSet>(set);
                tmogSet.Slots = set.Items.GroupBy(i => i.Slot).Select(i => new Slot
                {
                    Name      = i.Key.ToString(),
                    Items     = Mapper.Map<IEnumerable<Item>>(i),
                    Completed = slotMan.IsComplete(i.Key)
                });
                
                return Ok(tmogSet);
            }

            return NotFound();
        }

        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] int id)
        {
            var set = await service.Create(id);

            return CreatedAtRoute("tmog-sets", new { id = id }, set);
        }

        private void MapAsListItem(Entities.Set source, TMogSet target)
        {
            var slots = new SlotManager(source.Slots);

            target.Id = source.SetId;
            target.Name = source.Name;
            target.TotalSlots = slots.ActiveSlotCount;
            target.CompletedSlots = slots.CompletedSlotCount;
        }

        private void MapAsDetail(Entities.Set source, TMogSet target)
        {
            MapAsListItem(source, target);
            // group items by slot
            var result = source.Items.GroupBy(i => i.Slot);

        }
    }
}
