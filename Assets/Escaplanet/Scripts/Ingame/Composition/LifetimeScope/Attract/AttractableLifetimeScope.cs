using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.EntryPoint.Attract;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Attract
{
    public class AttractableLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var attractable = GetComponent<IAttractableCore>();
            builder.RegisterInstance(attractable);
            builder.RegisterEntryPoint<AttractableEntryPoint>(Lifetime.Scoped);
        }
    }
}