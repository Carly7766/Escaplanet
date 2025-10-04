using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerMovementCore
    {
        ScalarFloat MoveSpeed { get; }
        ScalarFloat Acceleration { get; }
        ScalarFloat MovementLerpAmount { get; }

        bool IsFlayingAway { get; set; }
        
        Vector2 Position { get; }
        Vector2 Velocity { get; }

        void Move(Vector2 velocity);
    }
}