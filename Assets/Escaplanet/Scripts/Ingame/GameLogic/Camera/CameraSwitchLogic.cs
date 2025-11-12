using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class CameraSwitchLogic : ICameraSwitchLogic
    {
        public void SwitchCamera(IMainCameraCore mainCamera, IVirtualCameraCore virtualCamera)
        {
            mainCamera.PreviousCamera = mainCamera.ActiveCamera;
            mainCamera.ActiveCamera = virtualCamera;

            if (mainCamera.PreviousCamera != null)
            {
                mainCamera.IsTransitioning = true;
                mainCamera.TransitionTimer = 0f;
            }
            else
            {
                mainCamera.IsTransitioning = false;
            }
        }
    }
}