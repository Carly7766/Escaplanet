using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;

namespace Escaplanet.Ingame.GameLogic.Camera.PlayerCamera
{
    public class PlayerCameraUpdateLogic : IPlayerCameraUpdateLogic
    {
        public void UpdatePlayerCamera(IPlayerCameraCore playerCamera, IPlayerMovementCore playerMovement)
        {
            var cameraState = playerCamera.State;
            cameraState.Position = playerMovement.Position;
            playerCamera.State = cameraState;
        }
    }
}