using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public interface IMainCameraSwitchLogic
    {
        void SwitchCamera(IMainCameraCore mainCamera, IVirtualCameraCore virtualCamera);
    }
}