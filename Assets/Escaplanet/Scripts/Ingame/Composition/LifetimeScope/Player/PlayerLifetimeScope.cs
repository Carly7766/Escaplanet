using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.EntryPoint.Player;
using Escaplanet.Ingame.GameLogic.Player;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Player
{
    public class PlayerLifetimeScope : VContainer.Unity.LifetimeScope
    {
        private IPlayerInputCore _playerInputCore;
        private IPlayerMovementCore _playerMovementCore;

        protected override void Awake()
        {
            _playerInputCore = GetComponent<IPlayerInputCore>();
            _playerMovementCore = GetComponent<IPlayerMovementCore>();
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerInputCore).AsSelf();
            builder.RegisterComponent(_playerMovementCore).AsSelf();

            builder.Register<PlayerMovementLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerJumpChargeLogic>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<PlayerEntryPoint>(Lifetime.Scoped);
        }
    }
}