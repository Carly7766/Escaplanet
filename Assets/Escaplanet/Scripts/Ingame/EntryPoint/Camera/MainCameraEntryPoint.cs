using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.GameLogic.Camera;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Camera
{
    public class MainCameraEntryPoint : IStartable, IPostLateTickable
    {
        private IMainCameraCore _mainCamera;
        private IPlayerCameraCore _playerCamera;

        private IMainCameraUpdateLogic _mainCameraUpdateLogic;
        private IMainCameraSwitchLogic _mainCameraSwitchLogic;

        public MainCameraEntryPoint(IMainCameraCore mainCamera, IPlayerCameraCore playerCamera,
            IMainCameraUpdateLogic mainCameraUpdateLogic, IMainCameraSwitchLogic mainCameraSwitchLogic)
        {
            _mainCamera = mainCamera;
            _playerCamera = playerCamera;
            _mainCameraUpdateLogic = mainCameraUpdateLogic;
            _mainCameraSwitchLogic = mainCameraSwitchLogic;
        }

        public void Start()
        {
            _mainCameraSwitchLogic.SwitchCamera(_mainCamera, _playerCamera);
        }

        public void PostLateTick()
        {
            _mainCameraUpdateLogic.LateUpdate(_mainCamera);
        }
    }
}