using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;

namespace Escaplanet.Ingame.GameLogic.Camera.PlayerCamera
{
    public class PlayerCameraUpdateLogic : IPlayerCameraUpdateLogic
    {
        public void UpdatePlayerCamera(IPlayerCameraCore cameraCore, IPlayerMovementCore playerMovementCore)
        {
            var cameraState = cameraCore.State;
            cameraState.Position = playerMovementCore.Position;
            cameraCore.State = cameraState;
        }
    }
}