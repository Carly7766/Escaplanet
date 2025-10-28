using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class MainCameraSwitchLogic : IMainCameraSwitchLogic
    {
        public void SwitchCamera(IMainCameraCore mainCamera, IVirtualCameraCore virtualCamera)
        {
            mainCamera.PreviousCamera = mainCamera.ActiveCamera;
            mainCamera.ActiveCamera = virtualCamera;
        }
    }
}