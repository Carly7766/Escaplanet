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

        public PlayerMovementLogic(IGlobalValuePort globalValuePort, IFloatMathPort floatMathPort)
        {
            _globalValuePort = globalValuePort;
            _floatMathPort = floatMathPort;
        }

        public void UpdateMovement(IAttractableCore attractable, IPlayerMovementCore playerMovement,
            IPlayerInputCore playerInput)
        {
            if (attractable.NearestSource == null) return;
            if (playerMovement.IsFlayingAway) return;

            var diff = playerMovement.Position - attractable.NearestSource.Position;

            var perpendicular = new Vector2(diff.Y, -diff.X);
            var perpendicularNormalized = perpendicular.Normalize();

            var perpendicularSpeed = Vector2.Dot(playerMovement.Velocity, perpendicularNormalized);

            if (_floatMathPort.Abs(perpendicularSpeed) > playerMovement.MoveSpeed)
            {
                playerMovement.IsFlayingAway = true;
                return;
            }

            var targetSpeed = playerInput.MoveInput * playerMovement.MoveSpeed;
            targetSpeed = _floatMathPort.Lerp(perpendicularSpeed, targetSpeed, playerMovement.MovementLerpAmount);

            var accelRate = playerMovement.Acceleration / playerMovement.MoveSpeed *
                            (1f / _globalValuePort.FixedDeltaTime);

            var speedDif = targetSpeed - perpendicularSpeed;
            var movement = speedDif * accelRate;

            playerMovement.Move(perpendicularNormalized * movement);
        }

        public void OnGround(IPlayerMovementCore playerMovement)
        {
            playerMovement.IsFlayingAway = false;
            playerMovement.IsJumping = false;
        }
    }
}