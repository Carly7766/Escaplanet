using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.EntryPoint.Attract;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Attract
{
    public class RotateAttractableLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var rotateAttractable = GetComponent<IRotateAttractableCore>();
            builder.RegisterComponent(rotateAttractable).AsSelf().As<IAttractableCore>();

            builder.RegisterEntryPoint<AttractableEntryPoint>(Lifetime.Scoped);
            builder.RegisterEntryPoint<RotateAttractableEntryPoint>(Lifetime.Scoped);
        }
    }
}