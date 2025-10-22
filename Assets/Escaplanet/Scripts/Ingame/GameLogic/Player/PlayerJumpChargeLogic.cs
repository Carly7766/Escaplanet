using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerJumpChargeLogic : IPlayerJumpChargeLogic
    {
        private IPlayerMovementCore _playerMovementCore;
        private IPlayerInputCore _playerInputCore;
        private IGlobalValuePort _globalValuePort;

        public PlayerJumpChargeLogic(IPlayerMovementCore playerMovementCore, IPlayerInputCore playerInputCore,
            IGlobalValuePort globalValuePort)
        {
            _playerMovementCore = playerMovementCore;
            _playerInputCore = playerInputCore;
            _globalValuePort = globalValuePort;
        }

        public void StartJumpCharge()
        {
            if (_playerMovementCore.IsJumping || _playerMovementCore.IsFlayingAway) return;
            _playerMovementCore.IsChargingJump = true;
        }

        public void Jump()
        {
            if (_playerMovementCore.IsJumping || _playerMovementCore.IsFlayingAway)
            {
                ResetJump();
                return;
            }

            _playerMovementCore.Jump(_playerMovementCore.Up *
                                     _playerMovementCore.JumpPower *
                                     _playerMovementCore.JumpPowerMultiplier);
            _playerMovementCore.IsJumping = true;
            ResetJump();
        }

        public void UpdateJumpCharge()
        {
            if (_playerMovementCore.IsChargingJump)
            {
                _playerMovementCore.JumpPower += _playerMovementCore.JumpChargeSpeed * _globalValuePort.DeltaTime;

                if (_playerMovementCore.JumpPower > _playerMovementCore.MaxJumpPower)
                {
                    _playerMovementCore.JumpPower = _playerMovementCore.MaxJumpPower;
                }
            }
        }

        public void ResetJump()
        {
            _playerMovementCore.IsChargingJump = false;
            _playerMovementCore.JumpPower = 0;
        }
    }
}