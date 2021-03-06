﻿using TMog.Entities;
using TMog.WowApi;

namespace TMog.Services.Infrastructure
{
    public class WowZoneMapProfile : AutoMapper.Profile
    {
        public WowZoneMapProfile()
        {
            CreateMap<IWowZone, Zone>()
                .ForMember(zone => zone.ZoneId, opt => opt.MapFrom(zone => zone.Id))
                .ForMember(zone => zone.Location, opt => opt.Ignore())
                .ForMember(zone => zone.Type, opt => opt.ResolveUsing(zone => 
                {
                    if (zone.IsDungeon)
                    {
                        return ZoneType.Dungeon;
                    }

                    if (zone.IsRaid)
                    {
                        return ZoneType.Raid;
                    }

                    return ZoneType.Normal;
                }));
            CreateMap<IWowLocation, Location>()
                .ForMember(location => location.LocationId, opt => opt.MapFrom(location => location.Id));
        }
    }
}
