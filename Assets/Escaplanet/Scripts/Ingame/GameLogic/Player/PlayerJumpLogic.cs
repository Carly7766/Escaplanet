using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerJumpLogic : IPlayerJumpLogic
    {
        private IPlayerMovementCore _playerMovementCore;
        private IGlobalValuePort _globalValuePort;

        public PlayerJumpLogic(IPlayerMovementCore playerMovementCore, IGlobalValuePort globalValuePort)
        {
            _playerMovementCore = playerMovementCore;
            _globalValuePort = globalValuePort;
        }

        public void OnJumpInput(InputState inputState)
        {
            switch (inputState)
            {
                case InputState.Down:
                    if (!_playerMovementCore.IsJumping || !_playerMovementCore.IsFlayingAway)
                    {
                        _playerMovementCore.IsJumpInputHeld = true;
                    }

                    break;
                case InputState.Hold:
                    if (_playerMovementCore.IsJumpInputHeld)
                    {
                        _playerMovementCore.IsJumpCharging = true;
                    }

                    break;
                case InputState.Up:
                    if (_playerMovementCore.IsJumpCharging)
                    {
                        _playerMovementCore.Jump(_playerMovementCore.Up *
                                                 _playerMovementCore.JumpPower *
                                                 _playerMovementCore.ChargeJumpPowerMultiplier);
                        ResetJumpCharge();
                    }
                    else
                    {
                        _playerMovementCore.Jump(_playerMovementCore.Up *
                                                 _playerMovementCore.MaxJumpPower *
                                                 _playerMovementCore.JumpPowerMultiplier);
                    }

                    _playerMovementCore.IsJumpInputHeld = false;
                    _playerMovementCore.IsJumping = true;

                    break;
            }
        }

        public void UpdateJump()
        {
            if (_playerMovementCore.IsJumpCharging)
            {
                _playerMovementCore.JumpPower += _playerMovementCore.JumpChargeSpeed * _globalValuePort.DeltaTime;

                if (_playerMovementCore.JumpPower > _playerMovementCore.MaxJumpPower)
                {
                    _playerMovementCore.JumpPower = _playerMovementCore.MaxJumpPower;
                }
            }
        }

        public void ResetJumpCharge()
        {
            _playerMovementCore.IsJumpCharging = false;
            _playerMovementCore.JumpPower = 0;
        }
    }
}