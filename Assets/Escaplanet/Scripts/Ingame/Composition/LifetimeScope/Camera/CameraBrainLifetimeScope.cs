using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.EntryPoint.Camera;
using Escaplanet.Ingame.GameLogic.Camera;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Camera
{
    public class CameraBrainLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var cameraBrain = GetComponent<ICameraBrainCore>();
            builder.RegisterInstance(cameraBrain);

            builder.Register<CameraUpdateLogic>(Lifetime.Singleton).As<ICameraUpdateLogic>();
            builder.Register<CameraSwitchLogic>(Lifetime.Singleton).As<ICameraSwitchLogic>();


            builder.RegisterEntryPoint<CameraBrainEntryPoint>(Lifetime.Scoped);
        }
    }
}