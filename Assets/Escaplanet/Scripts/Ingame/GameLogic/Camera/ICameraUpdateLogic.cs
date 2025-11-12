using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public interface ICameraUpdateLogic
    {
        void LateUpdate(IMainCameraCore mainCamera);
    }
}