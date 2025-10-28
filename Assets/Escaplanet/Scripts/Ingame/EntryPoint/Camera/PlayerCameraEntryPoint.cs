using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Camera.PlayerCamera;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Camera
{
    public class PlayerCameraEntryPoint : ILateTickable
    {
        private IPlayerCameraCore _playerCamera;
        private IPlayerMovementCore _playerMovement;
        private IPlayerCameraUpdateLogic _playerCameraUpdateLogic;

        public PlayerCameraEntryPoint(IPlayerCameraCore playerCamera, IPlayerMovementCore playerMovement,
            IPlayerCameraUpdateLogic playerCameraUpdateLogic)
        {
            _playerCamera = playerCamera;
            _playerMovement = playerMovement;
            _playerCameraUpdateLogic = playerCameraUpdateLogic;
        }

        public void LateTick()
        {
            _playerCameraUpdateLogic.UpdatePlayerCamera(_playerCamera, _playerMovement);
        }
    }
}