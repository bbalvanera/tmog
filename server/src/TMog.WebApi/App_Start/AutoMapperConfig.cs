using AutoMapper;
using TMog.Services;

namespace TMog.WebApi
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfiles(typeof(AutoMapperConfig).Assembly);
                config.AddProfiles(typeof(SetsService).Assembly);
            });
        }
    }
}
