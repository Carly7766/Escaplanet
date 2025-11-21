using Escaplanet.Result.EntryPoint;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Result.Composition
{
    public class ClearLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ClearEntryPoint>(Lifetime.Singleton);
        }
    }
}