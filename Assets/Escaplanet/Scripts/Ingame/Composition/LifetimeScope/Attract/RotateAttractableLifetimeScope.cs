using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.EntryPoint.Attract;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Attract
{
    public class RotateAttractableLifetimeScope : VContainer.Unity.LifetimeScope
    {
        private IRotateAttractableCore _rotateAttractable;

        protected override void Awake()
        {
            _rotateAttractable = GetComponent<IRotateAttractableCore>();
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_rotateAttractable).AsSelf().As<IAttractableCore>();

            builder.RegisterEntryPoint<AttractableEntryPoint>(Lifetime.Scoped);
            builder.RegisterEntryPoint<RotateAttractableEntryPoint>(Lifetime.Scoped);
        }
    }
}