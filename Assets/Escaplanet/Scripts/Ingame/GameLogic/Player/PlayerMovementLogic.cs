using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.Service;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerMovementLogic : IPlayerMovementLogic
    {
        public void UpdateMovement(IPlayerMovementCore playerMovement, IAttractableCore attractable,
            IPlayerInputCore playerInputCore, IUnityGlobalPort unityGlobalPort)
        {
            if (attractable.NearestSource == null) return;
            if (playerMovement.IsFlayingAway) return;

            var diff = playerMovement.Position - attractable.NearestSource.Position;

            var perpendicular = new Vector2(diff.Y, -diff.X);
            var perpendicularNormalized = perpendicular.Normalize();

            var perpendicularSpeed = Vector2.Dot(playerMovement.Velocity, perpendicularNormalized);

            if (MathFService.Abs(perpendicularSpeed) > playerMovement.MoveSpeed)
            {
                playerMovement.IsFlayingAway = true;
                return;
            }

            var targetSpeed = playerInputCore.MoveInput * playerMovement.MoveSpeed;
            targetSpeed = MathFService.Lerp(perpendicularSpeed, targetSpeed, playerMovement.MovementLerpAmount);

            var accelRate = playerMovement.Acceleration / playerMovement.MoveSpeed *
                            (ScalarFloat.One / unityGlobalPort.FixedDeltaTime);

            var speedDif = targetSpeed - perpendicularSpeed;
            var movement = speedDif * accelRate;

            playerMovement.Move(perpendicularNormalized * movement);
        }
    }
}