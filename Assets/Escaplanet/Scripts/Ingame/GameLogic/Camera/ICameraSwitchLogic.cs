using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public interface ICameraSwitchLogic
    {
        void SwitchCamera(IMainCameraCore mainCamera, IVirtualCameraCore virtualCamera);
    }
}