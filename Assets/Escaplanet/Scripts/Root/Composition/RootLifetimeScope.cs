using Escaplanet.Root.Adapter.TempSceneData.LastTimeResultData;
using Escaplanet.Root.Framework.TempSceneData.ResultData;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Root.Composition
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ResultDataStore>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ResultDataRepository>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}