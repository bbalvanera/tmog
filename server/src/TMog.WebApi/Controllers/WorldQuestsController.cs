using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TMog.Services;
using TMog.Services.Exceptions;
using TMog.WebApi.Models;

namespace TMog.WebApi.Controllers
{
    [RoutePrefix("world-quests")]
    public class WorldQuestsController: ApiController
    {
        private readonly IWorldQuestsService service;

        public WorldQuestsController(IWorldQuestsService service)
        {
            this.service = service;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await service.GetActive();
            var mapped = Mapper.Map<IEnumerable<WorldQuest>>(result);

            return Ok(new { worldQuests = mapped });
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IHttpActionResult> SubmitActive([FromBody] IEnumerable<RawWorldQuest> worldQuests)
        {
            if (worldQuests == null || worldQuests.Count() == 0)
            {
                return BadRequest("No data received");
            }

            Validate(worldQuests);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var wqs = Mapper.Map<IEnumerable<Entities.WorldQuest>>(worldQuests);
                var affected = await service.Save(wqs);

                return Ok(new { processedRecords = affected });
            }
            catch (DuplicateEntityException)
            {
                return BadRequest("Duplicate data received");
            }
            catch (ServiceException ex) when (ex.InnerException is EntityNotFoundException)
            {
                return BadRequest(ex.InnerException.Message);
            }
            catch (ServiceException)
            {
                //TODO: log exception
                return InternalServerError(new Exception("Internal Data Error"));
            }
        }

        [Route("{filter}/{id:int?}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFiltered(string filter, int? id)
        {
            return Ok($"received filter: {filter} and id: {id}");
        }
    }
}
