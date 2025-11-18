using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class CameraBackgroundFitLogic
    {
        public void UpdateFitCamera(IMainCameraCore mainCamera, ICameraBackgroundCore background)
        {
            background.FitBackgroundToCamera(mainCamera.CurrentState, mainCamera.Aspect);
        }
    }
}