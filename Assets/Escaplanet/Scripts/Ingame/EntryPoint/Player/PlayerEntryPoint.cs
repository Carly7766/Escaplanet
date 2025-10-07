using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Player;
using Escaplanet.Root.Common;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Player
{
    public class PlayerEntryPoint : IFixedTickable
    {
        private IAttractableCore _playerAttractableCore;
        private IPlayerInputCore _playerInputCore;
        private IPlayerMovementCore _playerMovementCore;

        private IPlayerMovementLogic _playerMovementLogic;

        public PlayerEntryPoint(IAttractableCore playerAttractableCore, IPlayerInputCore playerInputCore,
            IPlayerMovementCore playerMovementCore, IPlayerMovementLogic playerMovementLogic)
        {
            _playerAttractableCore = playerAttractableCore;
            _playerInputCore = playerInputCore;
            _playerMovementCore = playerMovementCore;
            _playerMovementLogic = playerMovementLogic;
        }

        public void FixedTick()
        {
            _playerMovementLogic.UpdateMovement(_playerAttractableCore, _playerMovementCore, _playerInputCore);
        }
    }
}