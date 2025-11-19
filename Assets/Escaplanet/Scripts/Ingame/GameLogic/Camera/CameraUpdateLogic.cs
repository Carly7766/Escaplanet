using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Root.Common;
using Escaplanet.Root.Core.Common;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class CameraUpdateLogic : ICameraUpdateLogic
    {
        private IGlobalValuePort _globalValuePort;

        public CameraUpdateLogic(IGlobalValuePort globalValuePort)
        {
            _globalValuePort = globalValuePort;
        }

        public void LateUpdate(IMainCameraCore mainCamera)
        {
            if (mainCamera.ActiveCamera == null) return;

            CameraState currentState;

            if (mainCamera.IsTransitioning)
            {
                mainCamera.TransitionTimer += _globalValuePort.DeltaTime;
                var t = mainCamera.TransitionTimer / mainCamera.TransitionDuration;

                currentState = CameraState.Lerp(
                    mainCamera.CurrentState,
                    mainCamera.ActiveCamera.State,
                    t);

                if (t >= 1f)
                {
                    mainCamera.IsTransitioning = false;
                }
            }
            else
            {
                currentState = mainCamera.ActiveCamera.State;
            }

            mainCamera.ApplyCameraState(currentState);
        }
    }
}