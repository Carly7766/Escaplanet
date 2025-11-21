using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Root.Core.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class CameraControlLogic : ICameraControlLogic
    {
        public void SwitchCamera(InputState inputState, IMainCameraCore mainCamera,
            ICameraSwitchLogic cameraSwitchLogic,
            IMainCameraControlCore mainCameraControl)
        {
            if (inputState == InputState.Down)
            {
                if (mainCamera.ActiveCamera == mainCameraControl.WorldCamera)
                    cameraSwitchLogic.SwitchCamera(mainCamera, mainCameraControl.PlayerCamera);
                else
                    cameraSwitchLogic.SwitchCamera(mainCamera, mainCameraControl.WorldCamera);
            }
        }
    }
}