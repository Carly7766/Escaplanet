using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.ValueObject;
using Escaplanet.Root.Core.Common;

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
            IPlayerInputCore playerInput, IPlayerAppearanceCore playerAppearance)
        {
            if (attractable.NearestSource == null) return;
            if (playerMovement.IsBlownAway) return;

            var diff = playerMovement.Position - attractable.NearestSource.Position;

            var perpendicular = new Vector2(diff.Y, -diff.X);
            var perpendicularNormalized = perpendicular.Normalize();

            var perpendicularSpeed = Vector2.Dot(playerMovement.Velocity, perpendicularNormalized);

            if (_floatMathPort.Abs(perpendicularSpeed) > playerMovement.MaxMoveSpeed)
            {
                playerMovement.IsBlownAway = true;
                return;
            }

            var targetSpeed = playerInput.MoveInput * playerMovement.MaxMoveSpeed;
            if (playerInput.MoveInput < 0f && playerAppearance.IsFacingRight)
            {
                playerAppearance.Flip(false);
            }
            else if (playerInput.MoveInput > 0f && !playerAppearance.IsFacingRight)
            {
                playerAppearance.Flip(true);
            }

            targetSpeed = _floatMathPort.Lerp(perpendicularSpeed, targetSpeed, playerMovement.MovementLerpFactor);

            var accelRate = playerMovement.MoveAcceleration / playerMovement.MaxMoveSpeed *
                            (1f / _globalValuePort.FixedDeltaTime);

            var speedDif = targetSpeed - perpendicularSpeed;
            var movement = speedDif * accelRate;

            playerMovement.Move(perpendicularNormalized * movement);
        }
    }
}