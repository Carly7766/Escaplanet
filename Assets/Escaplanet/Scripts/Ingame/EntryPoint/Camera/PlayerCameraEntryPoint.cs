using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Camera.PlayerCamera;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Camera
{
    public class PlayerCameraEntryPoint : ILateTickable
    {
        private IPlayerCameraCore _playerCameraCore;
        private IPlayerMovementCore _playerMovementCore;
        private IPlayerCameraUpdateLogic _playerCameraUpdateLogic;

        public PlayerCameraEntryPoint(IPlayerCameraCore playerCameraCore, IPlayerMovementCore playerMovementCore,
            IPlayerCameraUpdateLogic playerCameraUpdateLogic)
        {
            _playerCameraCore = playerCameraCore;
            _playerMovementCore = playerMovementCore;
            _playerCameraUpdateLogic = playerCameraUpdateLogic;
        }

        public void LateTick()
        {
            _playerCameraUpdateLogic.UpdatePlayerCamera(_playerCameraCore, _playerMovementCore);
        }
    }
}