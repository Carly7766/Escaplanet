using System;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
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

        private IPlayerMovementLogic _playerMovementLogic;
        private IPlayerJumpLogic _playerJumpLogic;
        private PlayerGroundDetectionLogic _playerGroundDetectionLogic;

        private CompositeDisposable _disposables = new();

        public PlayerEntryPoint(IAttractableCore playerAttractableCore, IPlayerInputCore playerInputCore,
            IPlayerMovementCore playerMovementCore, IPlayerMovementLogic playerMovementLogic,
            IPlayerJumpLogic playerJumpLogic, PlayerGroundDetectionLogic playerGroundDetectionLogic)
        {
            _playerAttractableCore = playerAttractableCore;
            _playerInputCore = playerInputCore;
            _playerMovementCore = playerMovementCore;
            _playerMovementLogic = playerMovementLogic;
            _playerJumpLogic = playerJumpLogic;
            _playerGroundDetectionLogic = playerGroundDetectionLogic;
        }

        public void Start()
        {
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
        }

        public void FixedTick()
        {
            _playerMovementLogic.UpdateMovement(_playerAttractableCore, _playerMovementCore, _playerInputCore);
            _playerJumpLogic.FixedUpdateJump(_playerMovementCore);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}