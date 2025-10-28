using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;

namespace Escaplanet.Ingame.GameLogic.Camera.PlayerCamera
{
    public interface IPlayerCameraUpdateLogic
    {
        void UpdatePlayerCamera(IPlayerCameraCore playerCamera, IPlayerMovementCore playerMovement);
    }
}