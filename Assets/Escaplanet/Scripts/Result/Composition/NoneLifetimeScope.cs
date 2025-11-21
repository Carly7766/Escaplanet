using Escaplanet.Result.EntryPoint;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Result.Composition
{
    public class NoneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<NoneEntryPoint>(Lifetime.Singleton);
        }
    }
}