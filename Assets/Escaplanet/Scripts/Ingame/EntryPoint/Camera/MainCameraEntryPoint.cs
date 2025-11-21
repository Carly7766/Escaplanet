using System;
using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Camera;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Camera
{
    public class MainCameraEntryPoint : IStartable, IPostStartable, IPostLateTickable, IDisposable
    {
        private readonly ICameraBackgroundCore _backgroundCore;

        private readonly CameraBackgroundFitLogic _cameraBackgroundFitLogic;
        private readonly ICameraControlLogic _cameraControlLogic;
        private readonly ICameraSwitchLogic _cameraSwitchLogic;

        private readonly ICameraUpdateLogic _cameraUpdateLogic;

        private readonly CompositeDisposable _disposables = new();
        private readonly IMainCameraCore _mainCamera;
        private readonly IMainCameraControlCore _mainCameraControl;
        private readonly IPlayerInputCore _playerInputCore;
        private readonly IWorldVirtualCameraCore _worldVirtualCamera;

        public MainCameraEntryPoint(IMainCameraCore mainCamera, IMainCameraControlCore mainCameraControl,
            IWorldVirtualCameraCore worldVirtualCamera, IPlayerInputCore playerInputCore,
            ICameraBackgroundCore backgroundCore, ICameraUpdateLogic cameraUpdateLogic,
            ICameraSwitchLogic cameraSwitchLogic, ICameraControlLogic cameraControlLogic,
            CameraBackgroundFitLogic cameraBackgroundFitLogic)
        {
            _mainCamera = mainCamera;
            _mainCameraControl = mainCameraControl;
            _worldVirtualCamera = worldVirtualCamera;
            _playerInputCore = playerInputCore;
            _backgroundCore = backgroundCore;
            _cameraUpdateLogic = cameraUpdateLogic;
            _cameraSwitchLogic = cameraSwitchLogic;
            _cameraControlLogic = cameraControlLogic;
            _cameraBackgroundFitLogic = cameraBackgroundFitLogic;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void PostLateTick()
        {
            _cameraUpdateLogic.LateUpdate(_mainCamera);
            _cameraBackgroundFitLogic.UpdateFitCamera(_mainCamera, _backgroundCore);
        }

        public void PostStart()
        {
            _cameraSwitchLogic.SwitchCamera(_mainCamera, _mainCameraControl.PlayerCamera);
        }

        public void Start()
        {
            _mainCameraControl.WorldCamera = _worldVirtualCamera;
            _playerInputCore.OnSwitchCameraInput.Subscribe(inputState =>
                {
                    _cameraControlLogic.SwitchCamera(inputState, _mainCamera, _cameraSwitchLogic, _mainCameraControl);
                }
            ).AddTo(_disposables);
        }
    }
}