using Escaplanet.Ingame.Core.Gimmick.BlackHole;
using Escaplanet.Ingame.EntryPoint.Gimmick;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Gimmick
{
    public class BlackHoleLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GetComponent<IBlackHoleCore>());

            builder.RegisterEntryPoint<BlackHoleEntryPoint>(Lifetime.Singleton);
        }
    }
}