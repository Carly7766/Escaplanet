using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Player;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Player
{
    public class PlayerEntryPoint : IStartable, ITickable, IFixedTickable
    {
        private IAttractableCore _playerAttractableCore;
        private IPlayerInputCore _playerInputCore;
        private IPlayerMovementCore _playerMovementCore;

        private IPlayerMovementLogic _playerMovementLogic;
        private IPlayerJumpChargeLogic _playerJumpChargeLogic;

        private CompositeDisposable _disposables = new();

        public PlayerEntryPoint(IAttractableCore playerAttractableCore, IPlayerInputCore playerInputCore,
            IPlayerMovementCore playerMovementCore, IPlayerMovementLogic playerMovementLogic,
            IPlayerJumpChargeLogic playerJumpChargeLogic)
        {
            _playerAttractableCore = playerAttractableCore;
            _playerInputCore = playerInputCore;
            _playerMovementCore = playerMovementCore;
            _playerMovementLogic = playerMovementLogic;
            _playerJumpChargeLogic = playerJumpChargeLogic;
        }

        public void Start()
        {
            _playerInputCore.OnJumpInputDown.Subscribe(_ => _playerJumpChargeLogic.StartJumpCharge())
                .AddTo(_disposables);
            _playerInputCore.OnJumpInputUp.Subscribe(_ => _playerJumpChargeLogic.Jump())
                .AddTo(_disposables);
        }

        public void Tick()
        {
            _playerJumpChargeLogic.UpdateJumpCharge();
        }

        public void FixedTick()
        {
            _playerMovementLogic.UpdateMovement(_playerAttractableCore, _playerMovementCore, _playerInputCore);
        }
    }
}