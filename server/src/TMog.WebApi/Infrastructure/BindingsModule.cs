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
            Bind<TMogDatabase>().ToSelf();

            // services
            Bind<ISetsService>().To<SetsService>();
            Bind<IZonesService>().To<ZonesService>();
            Bind<IItemsService>().To<ItemsService>();
        }
    }
}
