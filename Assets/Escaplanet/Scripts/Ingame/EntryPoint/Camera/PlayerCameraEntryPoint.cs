using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Camera.PlayerCamera;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Camera
{
    public class PlayerCameraEntryPoint : IStartable, ILateTickable
    {
        private readonly IMainCameraControlCore _mainCameraControlCore;
        private readonly IPlayerCameraCore _playerCamera;
        private readonly IPlayerCameraUpdateLogic _playerCameraUpdateLogic;
        private readonly IPlayerMovementCore _playerMovement;

        public PlayerCameraEntryPoint(IMainCameraControlCore mainCameraControlCore, IPlayerCameraCore playerCamera,
            IPlayerMovementCore playerMovement, IPlayerCameraUpdateLogic playerCameraUpdateLogic)
        {
            _mainCameraControlCore = mainCameraControlCore;
            _playerCamera = playerCamera;
            _playerMovement = playerMovement;
            _playerCameraUpdateLogic = playerCameraUpdateLogic;
        }

        public void LateTick()
        {
            _playerCameraUpdateLogic.UpdatePlayerCamera(_playerCamera, _playerMovement);
        }

        public void Start()
        {
            _mainCameraControlCore.PlayerCamera = _playerCamera;
        }
    }
}