using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Camera.PlayerCamera
{
    public class PlayerCameraUpdateLogic : IPlayerCameraUpdateLogic
    {
        public void UpdatePlayerCamera(IPlayerCameraCore playerCamera, IPlayerMovementCore playerMovement)
        {
            var cameraState = playerCamera.State;
            cameraState.Position = new Vector3(playerMovement.Position.X, playerMovement.Position.Y, -10);
            playerCamera.State = cameraState;
        }
    }
}