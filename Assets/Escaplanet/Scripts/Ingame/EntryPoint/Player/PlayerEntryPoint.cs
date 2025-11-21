using System;
using System.Threading;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.GameOver;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.Core.UI;
using Escaplanet.Ingame.GameLogic.GameOver;
using Escaplanet.Ingame.GameLogic.Player;
using Escaplanet.Root.Core;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Player
{
    public class PlayerEntryPoint : IStartable, ITickable, IFixedTickable, IDisposable
    {
        private readonly IGameInfoCore _gameInfoCore;

        private readonly IAttractableCore _playerAttractableCore;
        private readonly IPlayerInputCore _playerInputCore;
        private readonly ICountdownTextCore _countdownTextCore;
        private readonly IPlayerMovementCore _playerMovementCore;
        private readonly IPlayerAppearanceCore _playerAppearanceCore;
        private readonly IGameOverDetectableCore _playerGameOverDetectableCore;

        private readonly IPlayerMovementLogic _playerMovementLogic;
        private readonly IPlayerJumpLogic _playerJumpLogic;
        private readonly PlayerGroundDetectionLogic _playerGroundDetectionLogic;
        private readonly GameOverMiaDetectionLogic _gameOverMiaDetectionLogic;

        private readonly CompositeDisposable _disposables = new();
        private CancellationTokenSource _cancellationTokenSource;

        public PlayerEntryPoint(IGameInfoCore gameInfoCore, IAttractableCore playerAttractableCore,
            IPlayerInputCore playerInputCore, ICountdownTextCore countdownTextCore,
            IPlayerMovementCore playerMovementCore, IPlayerAppearanceCore playerAppearanceCore,
            IGameOverDetectableCore playerGameOverDetectableCore, IPlayerMovementLogic playerMovementLogic,
            IPlayerJumpLogic playerJumpLogic, PlayerGroundDetectionLogic playerGroundDetectionLogic,
            GameOverMiaDetectionLogic gameOverMiaDetectionLogic)
        {
            _gameInfoCore = gameInfoCore;
            _playerAttractableCore = playerAttractableCore;
            _playerInputCore = playerInputCore;
            _countdownTextCore = countdownTextCore;
            _playerMovementCore = playerMovementCore;
            _playerAppearanceCore = playerAppearanceCore;
            _playerGameOverDetectableCore = playerGameOverDetectableCore;
            _playerMovementLogic = playerMovementLogic;
            _playerJumpLogic = playerJumpLogic;
            _playerGroundDetectionLogic = playerGroundDetectionLogic;
            _gameOverMiaDetectionLogic = gameOverMiaDetectionLogic;
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _cancellationTokenSource.Cancel();
        }

        public void FixedTick()
        {
            _playerMovementLogic.UpdateMovement(_playerAttractableCore, _playerMovementCore, _playerInputCore,
                _playerAppearanceCore);
            _playerJumpLogic.FixedUpdateJump(_playerMovementCore);
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
            _gameOverMiaDetectionLogic.Update(
                _playerAttractableCore,
                _playerGameOverDetectableCore,
                _countdownTextCore,
                _gameInfoCore,
                _cancellationTokenSource.Token);
        }
    }
}