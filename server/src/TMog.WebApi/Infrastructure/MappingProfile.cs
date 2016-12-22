using TMog.Business;
using TMog.Entities;
using TMog.WebApi.Models;

namespace TMog.WebApi.Infrastructure
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Set, TMogSet>()
                .ForMember(target => target.TMogSetId, opt => opt.MapFrom(source => source.SetId))
                .ForMember(target => target.Slots, opt => opt.Ignore())
                .ForMember(target => target.TotalSlots, opt => opt.ResolveUsing(source =>
                {
                    var slots = new SlotManager(source.Slots);
                    return slots.ActiveSlotCount;
                }))
                .ForMember(target => target.CompletedSlots, opt => opt.ResolveUsing(source => 
                {
                    var slots = new SlotManager(source.Slots);
                    return slots.CompletedSlotCount;
                }));

            CreateMap<Entities.Item, Models.Item>();
            CreateMap<Entities.Source, Models.Source>();
            CreateMap<Entities.Zone, Models.Zone>();
        }
    }
}
