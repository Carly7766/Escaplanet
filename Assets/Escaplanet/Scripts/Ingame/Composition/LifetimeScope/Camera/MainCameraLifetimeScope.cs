using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.EntryPoint.Camera;
using Escaplanet.Ingame.GameLogic.Camera;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Camera
{
    public class MainCameraLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var cameraBrain = GetComponent<IMainCameraCore>();
            builder.RegisterInstance(cameraBrain);

            builder.RegisterEntryPoint<MainCameraEntryPoint>(Lifetime.Scoped);
        }
    }
}