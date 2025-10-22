using Escaplanet.Ingame.GameLogic.Camera;
using VContainer;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Camera
{
    public class CameraLogicLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CameraUpdateLogic>(Lifetime.Singleton).As<ICameraUpdateLogic>();
            builder.Register<CameraSwitchLogic>(Lifetime.Singleton).As<ICameraSwitchLogic>();
        }
    }
}