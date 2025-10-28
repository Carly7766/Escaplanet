using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public interface IMainCameraUpdateLogic
    {
        void LateUpdate(IMainCameraCore mainCamera);
    }
}