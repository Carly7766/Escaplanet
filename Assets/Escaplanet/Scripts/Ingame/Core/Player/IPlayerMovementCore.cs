using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerMovementCore
    {
        float MoveSpeed { get; }
        float Acceleration { get; }
        float MovementLerpAmount { get; }

        bool IsFlayingAway { get; set; }

        Vector2 Position { get; }
        Vector2 Velocity { get; }

        void Move(Vector2 velocity);
    }
}