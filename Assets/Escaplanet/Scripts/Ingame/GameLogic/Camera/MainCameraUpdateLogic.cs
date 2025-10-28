using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Root.Common;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class MainCameraUpdateLogic : IMainCameraUpdateLogic
    {
        private IGlobalValuePort _globalValuePort;

        public MainCameraUpdateLogic(IGlobalValuePort globalValuePort)
        {
            _globalValuePort = globalValuePort;
        }

        public void LateUpdate(IMainCameraCore mainCamera)
        {
            if (mainCamera.ActiveCamera == null) return;

            // cameraBrainCore.CurrentState = cameraBrainCore.ActiveCamera.State;

            CameraState activeState;

            if (mainCamera.PreviousCamera != null)
            {
                activeState = CameraState.Lerp(
                    mainCamera.PreviousCamera.State,
                    mainCamera.ActiveCamera.State,
                    _globalValuePort.DeltaTime);
            }
            else
            {
                activeState = mainCamera.ActiveCamera.State;
            }

            mainCamera.ApplyCameraState(activeState);
        }
    }
}