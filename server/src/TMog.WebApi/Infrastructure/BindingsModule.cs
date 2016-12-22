using AutoMapper;
using Ninject;
using Ninject.Modules;
using TMog.Data;
using TMog.Services;
using TMog.WowApi;
using TMog.WowheadApi;

namespace TMog.WebApi.Infrastructure
{
    public class BindingsModule : NinjectModule
    {
        public override void Load()
        {
            // data
            Bind<IWowheadProvider>().To<WowheadProvider>();
            Bind<IWowProvider>().To<WowProvider>();
            Bind<ITMogDatabase>().To<TMogDatabase>();

            // services
            Bind<ISetsService>().To<SetsService>();

            SetAutoMapperBindings();
        }

        private void SetAutoMapperBindings()
        {
            Mapper.Initialize(config => 
            {
                config.ConstructServicesUsing(type => Kernel.Get(type));
            });
        }
    }
}
