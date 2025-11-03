using Escaplanet.Root.Common.ValueObject;
using R3;

namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerMovementCore
    {
        // Movement Properties
        float MaxMoveSpeed { get; }
        float MoveAcceleration { get; }
        float MovementLerpFactor { get; }

        bool IsBlownAway { get; set; }


        // Jump Properties
        float MaxJumpPower { get; }
        float JumpPowerChargeSpeed { get; }
        float UnchargedJumpMultiplier { get; }
        float ChargedJumpMultiplier { get; }

        float JumpCharge { get; set; }

        bool IsJumpInputHeld { get; set; }
        bool IsJumpCharging { get; set; }
        bool IsJumping { get; set; }


        // Unity Properties
        Vector2 Up { get; }
        Vector2 Position { get; }
        Vector2 Velocity { get; }

        Observable<Unit> OnGrounded { get; }

        void Move(Vector2 velocity);
        void Jump(Vector2 force);
    }
}