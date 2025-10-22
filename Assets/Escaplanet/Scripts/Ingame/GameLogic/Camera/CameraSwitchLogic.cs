using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public class CameraSwitchLogic : ICameraSwitchLogic
    {
        public void SwitchCamera(ICameraBrainCore cameraBrain, IVirtualCameraCore virtualCamera)
        {
            cameraBrain.PreviousCamera = cameraBrain.ActiveCamera;
            cameraBrain.ActiveCamera = virtualCamera;
        }
    }
}