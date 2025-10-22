using Escaplanet.Root.Common.ValueObject;
using R3;

namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerMovementCore
    {
        float MoveSpeed { get; }
        float Acceleration { get; }
        float MovementLerpAmount { get; }

        bool IsFlayingAway { get; set; }
        bool IsJumping { get; set; }

        float MaxJumpPower { get; }
        float JumpChargeSpeed { get; }
        float JumpPowerMultiplier { get; }
        float JumpPower { get; set; }
        bool IsChargingJump { get; set; }

        Vector2 Position { get; }
        Vector2 Up { get; }
        Vector2 Velocity { get; }

        Observable<Unit> OnGrounded { get; }

        void Move(Vector2 velocity);
        void Jump(Vector2 force);
    }
}