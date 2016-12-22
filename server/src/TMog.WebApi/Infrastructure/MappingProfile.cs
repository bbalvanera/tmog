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
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.SetId))
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

            CreateMap<Entities.Item, Models.Item>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.ItemId));

            CreateMap<Entities.Source, Models.Source>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.SourceId));

            CreateMap<Entities.Zone, Models.Zone>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.ZoneId));
        }
    }
}
