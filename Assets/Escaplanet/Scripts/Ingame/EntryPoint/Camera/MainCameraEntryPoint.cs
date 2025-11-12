using System;
using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Camera;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Camera
{
    public class MainCameraEntryPoint : IStartable, IPostStartable, IPostLateTickable, IDisposable
    {
        private IMainCameraCore _mainCamera;
        private IMainCameraControlCore _mainCameraControl;
        private IWorldVirtualCameraCore _worldVirtualCamera;
        private IPlayerInputCore _playerInputCore;
        private ICameraUpdateLogic _cameraUpdateLogic;
        private ICameraSwitchLogic _cameraSwitchLogic;
        private ICameraControlLogic _cameraControlLogic;
        private CompositeDisposable _disposables = new();

        public MainCameraEntryPoint(IMainCameraCore mainCamera, IMainCameraControlCore mainCameraControl,
            IWorldVirtualCameraCore worldVirtualCamera, IPlayerInputCore playerInputCore,
            ICameraUpdateLogic cameraUpdateLogic, ICameraSwitchLogic cameraSwitchLogic,
            ICameraControlLogic cameraControlLogic)
        {
            _mainCamera = mainCamera;
            _mainCameraControl = mainCameraControl;
            _worldVirtualCamera = worldVirtualCamera;
            _playerInputCore = playerInputCore;
            _cameraUpdateLogic = cameraUpdateLogic;
            _cameraSwitchLogic = cameraSwitchLogic;
            _cameraControlLogic = cameraControlLogic;
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

        public void PostStart()
        {
            _cameraSwitchLogic.SwitchCamera(_mainCamera, _mainCameraControl.PlayerCamera);
        }

        public void PostLateTick()
        {
            _cameraUpdateLogic.LateUpdate(_mainCamera);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}