using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Camera
{
    public interface ICameraControlLogic
    {
        void SwitchCamera(InputState inputState, IMainCameraCore mainCamera, ICameraSwitchLogic cameraSwitchLogic,
            IMainCameraControlCore mainCameraControl);
    }
}