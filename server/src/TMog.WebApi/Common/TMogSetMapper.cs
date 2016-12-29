using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TMog.Business;
using TMog.WebApi.Models;

namespace TMog.WebApi.Common
{
    public static class TMogSetMapper
    {
        public static TMogSet FromSet(Entities.Set set)
        {
            var slotMan = new SlotManager(set.Slots);
            var tmogSet = Mapper.Map<TMogSet>(set);
            tmogSet.Slots = set.Items.GroupBy(i => i.Slot).Select(i => new Slot
            {
                Name = i.Key.ToString(),
                Items = Mapper.Map<IEnumerable<Item>>(i),
                Complete = slotMan.IsComplete(i.Key)
            });

            return tmogSet;
        }
    }
}
