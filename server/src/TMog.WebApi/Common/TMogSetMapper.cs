using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TMog.Business;
using TMog.WebApi.Models;

namespace TMog.WebApi.Common
{
    public static class TMogSetMapper
    {
        public static TmogSet FromSet(Entities.Set set)
        {
            var slotMan = new SlotManager(set.Slots);
            var tmogSet = Mapper.Map<TmogSet>(set);
            tmogSet.Slots = set.Items
                .GroupBy(i => i.Slot)
                .OrderBy(i => slotMan.IsComplete(i.Key))
                .ThenBy(i => i.Key.ToString())
                .Select(i => new Slot
            {
                Name = i.Key.ToString(),
                Items = Mapper.Map<IEnumerable<Item>>(i),
                Complete = slotMan.IsComplete(i.Key)
            });

            return tmogSet;
        }
    }
}
