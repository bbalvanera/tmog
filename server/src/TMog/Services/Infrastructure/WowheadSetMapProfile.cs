using System.Linq;
using TMog.Business;
using TMog.Entities;
using TMog.WowheadApi;

namespace TMog.Services.Infrastructure
{
    public class WowheadSetMapProfile : AutoMapper.Profile
    {
        public WowheadSetMapProfile()
        {
            CreateMap<IWowheadSet, Set>()
                .ForMember(dest => dest.SetId, opt => opt.MapFrom(source => source.WowheadSetId))
                .ForMember(dest => dest.Slots, opt => opt.ResolveUsing(source => SlotManager.FromRange(source.Items.Select(i => i.Slot).Distinct())))
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<IWowheadItem, Item>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Source, opt => opt.Ignore())
                .ForMember(dest => dest.Quality, opt => opt.ResolveUsing(source => {
                    if (!source.Quality.HasValue)
                    {
                        return QualityType.Unknown;
                    }

                    return (QualityType)source.Quality.Value;
                }));

            CreateMap<IWowheadItemSource, Source>()
                .ForMember(dest => dest.SourceId, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Zone, opt => opt.Ignore())
                .ForMember(dest => dest.ZoneId, opt => opt.ResolveUsing(source => source.Zone));
        }
    }
}
