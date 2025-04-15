using Escaplanet.Ingame.Framework.Planet;
using Escaplanet.Ingame.System.Planet;
using Escaplanet.Ingame.System.Planet.AttractSystem;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.LifeTimeScope.Planet
{
    public class PlanetLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var planetAttractSources = FindObjectsOfType<PlanetAttractSourceEntity>();
            foreach (var planetAttractSource in planetAttractSources)
            {
                builder.Register(_ => planetAttractSource, Lifetime.Scoped).AsImplementedInterfaces();
            }

            builder.Register<PlanetAttractSystem>(Lifetime.Scoped).AsImplementedInterfaces();

            builder.RegisterEntryPoint<PlanetEntryPoint>(Lifetime.Scoped);
        }
    }
}