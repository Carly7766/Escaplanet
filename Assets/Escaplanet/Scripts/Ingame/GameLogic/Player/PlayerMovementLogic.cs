using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerMovementLogic : IPlayerMovementLogic
    {
        private IGlobalValuePort _globalValuePort;
        private IFloatMathPort _floatMathPort;

        private IAttractableCore _attractable;
        private IPlayerMovementCore _playerMovement;
        private IPlayerInputCore _playerInput;

        public PlayerMovementLogic(IGlobalValuePort globalValuePort, IFloatMathPort floatMathPort,
            IAttractableCore attractable, IPlayerMovementCore playerMovement, IPlayerInputCore playerInput)
        {
            _globalValuePort = globalValuePort;
            _floatMathPort = floatMathPort;
            _attractable = attractable;
            _playerMovement = playerMovement;
            _playerInput = playerInput;
        }


        public void UpdateMovement()
        {
            if (_attractable.NearestSource == null) return;
            if (_playerMovement.IsFlayingAway) return;

            var diff = _playerMovement.Position - _attractable.NearestSource.Position;

            var perpendicular = new Vector2(diff.Y, -diff.X);
            var perpendicularNormalized = perpendicular.Normalize();

            var perpendicularSpeed = Vector2.Dot(_playerMovement.Velocity, perpendicularNormalized);

            if (_floatMathPort.Abs(perpendicularSpeed) > _playerMovement.MoveSpeed)
            {
                _playerMovement.IsFlayingAway = true;
                return;
            }

            var targetSpeed = _playerInput.MoveInput * _playerMovement.MoveSpeed;
            targetSpeed = _floatMathPort.Lerp(perpendicularSpeed, targetSpeed, _playerMovement.MovementLerpAmount);

            var accelRate = _playerMovement.Acceleration / _playerMovement.MoveSpeed *
                            (1f / _globalValuePort.FixedDeltaTime);

            var speedDif = targetSpeed - perpendicularSpeed;
            var movement = speedDif * accelRate;

            _playerMovement.Move(perpendicularNormalized * movement);
        }
    }
}