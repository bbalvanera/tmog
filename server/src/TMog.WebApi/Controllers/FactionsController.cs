using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TMog.Services;
using TMog.WebApi.Models;

namespace TMog.WebApi.Controllers
{
    public class FactionsController : ApiController
    {
        private readonly IFactionsService service;

        public FactionsController(IFactionsService service)
        {
            this.service = service;
        }

        public async Task<IHttpActionResult> Post([FromBody] Faction[] factions)
        {
            if (factions == null || factions.Length == 0)
            {
                return BadRequest("No data received");
            }

            try
            {
                var dataFactions = Mapper.Map<IEnumerable<Entities.Faction>>(factions);
                await service.AddAll(dataFactions);

                return Ok();
            }
            catch (DuplicateEntityException)
            {
                return BadRequest("Duplicate data received");
            }
            catch (ServiceException)
            {
                // log exception
                return InternalServerError(new Exception("Internal Data Error"));
            }
        }
    }
}
