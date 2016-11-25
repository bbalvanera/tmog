using Ninject.Modules;
using TMog.Data;
using TMog.Services;

namespace TMog.WebApi.Infrastructure
{
    public class BindingsModule : NinjectModule
    {
        public override void Load()
        {
            // data
            Bind<ITMogDatabase>().To<TMogDatabase>();

            // services
            Bind<ISetsService>().To<SetsService>();
        }
    }
}
