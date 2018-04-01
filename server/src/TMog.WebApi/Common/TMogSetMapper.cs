using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TMog.Common;
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
                .ThenBy(i => (int)i.Key)
                .Select(i => new Slot
                {
                    SlotNumber = (int)i.Key,
                    SlotName   = i.Key.ToString(),
                    Items      = Mapper.Map<IEnumerable<Item>>(i),
                    Complete   = slotMan.IsComplete(i.Key)
                });

            return tmogSet;
        }
    }
}
