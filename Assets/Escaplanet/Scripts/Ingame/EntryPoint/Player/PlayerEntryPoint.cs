using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.GameOver;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.Core.UI;
using Escaplanet.Ingame.GameLogic.GameOver;
using Escaplanet.Ingame.GameLogic.Player;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Player
{
    public class PlayerEntryPoint : IStartable, ITickable, IFixedTickable, IDisposable
    {
        private IAttractableCore _playerAttractableCore;
        private IPlayerInputCore _playerInputCore;
        private IPlayerMovementCore _playerMovementCore;
        private IGameOverDetectableCore _playerGameOverDetectableCore;

        private ICountdownTextCore _countdownTextCore;

        private IPlayerMovementLogic _playerMovementLogic;
        private IPlayerJumpLogic _playerJumpLogic;
        private PlayerGroundDetectionLogic _playerGroundDetectionLogic;
        private GameOverMiaDetectionLogic _gameOverMiaDetectionLogic;

        private CancellationTokenSource _cancellationTokenSource;
        private CompositeDisposable _disposables = new();

        public PlayerEntryPoint(IAttractableCore playerAttractableCore, IPlayerInputCore playerInputCore,
            IPlayerMovementCore playerMovementCore, ICountdownTextCore countdownTextCore,
            IPlayerMovementLogic playerMovementLogic, IPlayerJumpLogic playerJumpLogic,
            PlayerGroundDetectionLogic playerGroundDetectionLogic, GameOverMiaDetectionLogic gameOverMiaDetectionLogic,
            IGameOverDetectableCore playerGameOverDetectableCore)
        {
            _playerAttractableCore = playerAttractableCore;
            _playerInputCore = playerInputCore;
            _playerMovementCore = playerMovementCore;
            _countdownTextCore = countdownTextCore;
            _playerMovementLogic = playerMovementLogic;
            _playerJumpLogic = playerJumpLogic;
            _playerGroundDetectionLogic = playerGroundDetectionLogic;
            _gameOverMiaDetectionLogic = gameOverMiaDetectionLogic;
            _playerGameOverDetectableCore = playerGameOverDetectableCore;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _playerMovementCore.OnGrounded
                .Subscribe(_ => _playerGroundDetectionLogic.OnGroundDetected(_playerMovementCore))
                .AddTo(_disposables);

            _playerInputCore.OnJumpInput
                .Subscribe(inputState => _playerJumpLogic.OnJumpInput(_playerMovementCore, inputState))
                .AddTo(_disposables);
        }

        public void Tick()
        {
            _playerJumpLogic.UpdateJumpCharge(_playerMovementCore);
            _gameOverMiaDetectionLogic.Update(_playerAttractableCore, _playerGameOverDetectableCore, _countdownTextCore,
                _cancellationTokenSource.Token);
        }

        public void FixedTick()
        {
            _playerMovementLogic.UpdateMovement(_playerAttractableCore, _playerMovementCore, _playerInputCore);
            _playerJumpLogic.FixedUpdateJump(_playerMovementCore);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _cancellationTokenSource.Cancel();
        }
    }
}