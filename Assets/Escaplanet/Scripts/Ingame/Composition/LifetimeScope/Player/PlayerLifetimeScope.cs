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

        protected override void Configure(IContainerBuilder builder)
        {
            _playerInputCore = GetComponent<IPlayerInputCore>();
            _playerMovementCore = GetComponent<IPlayerMovementCore>();

            builder.RegisterComponent(_playerInputCore).AsSelf();
            builder.RegisterComponent(_playerMovementCore).AsSelf();

            builder.RegisterEntryPoint<PlayerEntryPoint>(Lifetime.Scoped);
        }
    }
}