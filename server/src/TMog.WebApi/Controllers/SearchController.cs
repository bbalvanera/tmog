using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TMog.Business;
using TMog.Services;
using TMog.WebApi.Models;

namespace TMog.WebApi.Controllers
{
    public class SearchController : ApiController
    {
        private readonly IItemsService service;

        public SearchController(IItemsService service)
        {
            this.service = service;
        }

        public async Task<IHttpActionResult> Get(string q)
        {
            if (!string.IsNullOrWhiteSpace(q))
            {
                var searchedItems = await service.SearchItems(q);
                var retVal = new List<Item>();

                if (searchedItems.Any())
                {
                    retVal = Map(searchedItems);
                }

                return Ok(new { results = retVal });
            }

            return Ok(new { results = new object[0] });
        }

        private List<Item> Map(IEnumerable<Entities.Item> searchedItems)
        {
            List<Item> map = new List<Item>();

            foreach (var searchItem in searchedItems)
            {
                var item = Mapper.Map<Item>(searchItem);
                var tmogSets = new List<TmogSet>();

                if (searchItem.Sets != null)
                {
                    foreach (var set in searchItem.Sets)
                    {
                        var tmogSet = Mapper.Map<TmogSet>(set);
                        tmogSet.Slots = new List<Slot>
                        {
                            new Slot
                            {
                                Name = searchItem.Slot.ToString(),
                                Complete = new SlotManager(set.Slots).IsComplete(searchItem.Slot)
                            }
                        };

                        tmogSets.Add(tmogSet);
                    }
                }

                item.Sets = tmogSets;
                map.Add(item);
            }

            return map;
        }
    }
}
