using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
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

        public async Task<IEnumerable<Item>> Get(string q)
        {
            if (!string.IsNullOrWhiteSpace(q))
            {
                var searchedItems = await service.SearchItems(q);
                var retVal = new List<Item>();

                if (searchedItems.Any())
                {
                    retVal = Map(searchedItems);
                }

                return retVal;
            }

            return Enumerable.Empty<Item>();
        }

        private List<Item> Map(IEnumerable<Entities.Item> searchedItems)
        {
            List<Item> map = new List<Item>();

            foreach (var searchItem in searchedItems)
            {
                var item = Mapper.Map<Item>(searchItem);
                var tmogSets = new List<TMogSet>();

                if (searchItem.Sets != null)
                {
                    foreach (var set in searchItem.Sets)
                    {
                        var tmogSet = Mapper.Map<TMogSet>(set);
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
