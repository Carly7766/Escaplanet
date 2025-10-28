using Escaplanet.Ingame.GameLogic.Attract;
using Escaplanet.Ingame.GameLogic.Camera;
using Escaplanet.Ingame.GameLogic.Player;
using VContainer;

namespace Escaplanet.Ingame.Composition.LifetimeScope
{
    public class IngameLogicLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // AttractSource/Attractable Logic
            builder.Register<AttractUpdateLogic>(Lifetime.Singleton)
                .As<IAttractUpdateLogic>();
            builder.Register<RotateUpdateLogic>(Lifetime.Singleton)
                .As<IRotateUpdateLogic>();
            builder.Register<AttractAreaDetectionLogic>(Lifetime.Singleton)
                .As<IAttractAreaDetectionLogic>();

            // Player Logic
            builder.Register<PlayerMovementLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerJumpLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerGroundDetectionLogic>(Lifetime.Singleton).AsSelf();

            // Camera Logic
            builder.Register<MainCameraUpdateLogic>(Lifetime.Singleton).As<IMainCameraUpdateLogic>();
            builder.Register<MainCameraSwitchLogic>(Lifetime.Singleton).As<IMainCameraSwitchLogic>();
        }
    }
}