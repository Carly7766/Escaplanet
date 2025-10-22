using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core.Camera;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public interface ICameraSwitchLogic
    {
        void SwitchCamera(ICameraBrainCore cameraBrain, IVirtualCameraCore virtualCamera);
    }
}