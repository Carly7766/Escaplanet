using Escaplanet.Ingame.Core.GameOver;
using Escaplanet.Ingame.GameLogic.Attract;
using Escaplanet.Ingame.GameLogic.Camera;
using Escaplanet.Ingame.GameLogic.GameOver;
using Escaplanet.Ingame.GameLogic.Player;
using Escaplanet.Ingame.Presentation.GameOver;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope
{
    public class IngameLogicLifetimeScope : VContainer.Unity.LifetimeScope
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

            // MainCamera Logic
            builder.Register<MainCameraUpdateLogic>(Lifetime.Singleton).As<IMainCameraUpdateLogic>();
            builder.Register<MainCameraSwitchLogic>(Lifetime.Singleton).As<IMainCameraSwitchLogic>();

            // GameOver Logic
            builder.RegisterInstance(_gameOverPolicyScriptableObject).As<IGameOverPolicy>();
            builder.RegisterComponent(_gameOverLogicComponent).As<IGameOverLogicCore>();
            builder.Register<GameOverLogic>(Lifetime.Singleton).AsSelf();
            builder.Register<GameOverMiaDetectionLogic>(Lifetime.Singleton).AsSelf();
        }
    }
}