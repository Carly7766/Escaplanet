using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.GameLogic.Camera;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Camera
{
    public class CameraBrainEntryPoint : IStartable, IPostLateTickable
    {
        private ICameraBrainCore _cameraBrainCore;
        private IPlayerCameraCore _playerCameraCore;

        private ICameraUpdateLogic _cameraUpdateLogic;
        private ICameraSwitchLogic _cameraSwitchLogic;

        public CameraBrainEntryPoint(ICameraBrainCore cameraBrainCore, IPlayerCameraCore playerCameraCore,
            ICameraUpdateLogic cameraUpdateLogic, ICameraSwitchLogic cameraSwitchLogic)
        {
            _cameraBrainCore = cameraBrainCore;
            _playerCameraCore = playerCameraCore;
            _cameraUpdateLogic = cameraUpdateLogic;
            _cameraSwitchLogic = cameraSwitchLogic;
        }

        public void Start()
        {
            _cameraSwitchLogic.SwitchCamera(_cameraBrainCore, _playerCameraCore);
        }

        public void PostLateTick()
        {
            _cameraUpdateLogic.LateUpdate(_cameraBrainCore);
        }
    }
}