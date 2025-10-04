using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Player;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Player
{
    public class PlayerLifetimeScope : VContainer.Unity.LifetimeScope
    {
        private IRotateAttractableCore _rotateAttractableCore;
        private IPlayerMovementCore _playerMovementCore;
        private IPlayerInputCore _playerInputCore;

        protected override void Awake()
        {
            _rotateAttractableCore = GetComponent<IRotateAttractableCore>();
            _playerMovementCore = GetComponent<IPlayerMovementCore>();
            _playerInputCore = GetComponent<IPlayerInputCore>();

            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_rotateAttractableCore).AsSelf();
            builder.RegisterInstance(_playerMovementCore).AsSelf();
            builder.RegisterInstance(_playerInputCore).AsSelf();

            builder.Register<PlayerMovementLogic>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<Ingame.EntryPoint.Player.PlayerEntryPoint>(Lifetime.Singleton);
        }
    }
}