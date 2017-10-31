using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TMog.Services;
using TMog.WebApi.Common;
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
        public async Task<IHttpActionResult> Get()
        {
            var results = await service.GetAll();
            var mapped  = Mapper.Map<IEnumerable<TmogSet>>(results);

            return Ok(new { sets = mapped });
        }

        [Route("{id:int}", Name = "tmog-sets")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var set = await service.GetById(id);
            if (set != null)
            {

                TmogSet tmogSet = TMogSetMapper.FromSet(set);

                return Ok(tmogSet);
            }

            return NotFound();
        }

        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] int id)
        {
            try
            {
                var set = await service.Create(id);

                return Created($"tmog-sets/{id}", "");
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName == "setId")
                {
                    return BadRequest("461");
                }

                throw;
            }
        }

        [Route("")]
        public async Task<IHttpActionResult> Delete([FromBody] int id)
        {
            if (id > 0)
            {
                await service.Delete(id);
            }

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}/slot/{slot}")]
        public async Task<IHttpActionResult> UpdateSlotCompletion(int id, string slot, [FromBody] string status)
        {
            Entities.SlotType slotType;
            if (!Enum.TryParse(slot, true, out slotType))
            {
                return BadRequest("Invalid slot");
            }            

            try
            {
                await service.MarkSetSlotCompletionStatus(id, slotType, status != "false");
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName == "setId")
                {
                    return BadRequest(ex.Message);
                }

                throw;
            }

            return Ok();
        }
    }
}
