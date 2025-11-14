using Escaplanet.Ingame.GameLogic.Attract;
using Escaplanet.Ingame.GameLogic.Camera;
using Escaplanet.Ingame.GameLogic.Camera.PlayerCamera;
using Escaplanet.Ingame.GameLogic.GameOver;
using Escaplanet.Ingame.GameLogic.Player;
using Escaplanet.Ingame.Presentation.Camera;
using Escaplanet.Ingame.Presentation.GameOver;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope
{
    public class IngameLifetimeScope : VContainer.Unity.LifetimeScope
    {
        [SerializeField] private GameOverPolicyScriptableObject _gameOverPolicyScriptableObject;
        [SerializeField] private GameOverLogicComponent _gameOverLogicComponent;

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

            // World Camera
            builder.RegisterComponent(FindObjectOfType<WorldCameraComponent>()).AsImplementedInterfaces();

            // MainCamera Logic
            builder.Register<CameraUpdateLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CameraSwitchLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CameraControlLogic>(Lifetime.Singleton).AsImplementedInterfaces();

            // PlayerCamera Logic
            builder.Register<PlayerCameraUpdateLogic>(Lifetime.Scoped).AsImplementedInterfaces();

            // GameOver Logic
            builder.RegisterInstance(_gameOverPolicyScriptableObject).AsImplementedInterfaces();
            builder.RegisterComponent(_gameOverLogicComponent).AsImplementedInterfaces();
            builder.Register<GameOverLogic>(Lifetime.Singleton).AsSelf();
            builder.Register<GameOverMiaDetectionLogic>(Lifetime.Singleton).AsSelf();
        }
    }
}