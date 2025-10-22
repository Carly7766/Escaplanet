using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Root.Common;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class CameraUpdateLogic : ICameraUpdateLogic
    {
        private IGlobalValuePort _globalValuePort;

        public CameraUpdateLogic(IGlobalValuePort globalValuePort)
        {
            _globalValuePort = globalValuePort;
        }

        public void LateUpdate(ICameraBrainCore cameraBrain)
        {
            if (cameraBrain.ActiveCamera == null) return;

            // cameraBrainCore.CurrentState = cameraBrainCore.ActiveCamera.State;

            CameraState activeState;

            if (cameraBrain.PreviousCamera != null)
            {
                activeState = CameraState.Lerp(
                    cameraBrain.PreviousCamera.State,
                    cameraBrain.ActiveCamera.State,
                    _globalValuePort.DeltaTime);
            }
            else
            {
                activeState = cameraBrain.ActiveCamera.State;
            }

            cameraBrain.ApplyCameraState(activeState);
        }
    }
}