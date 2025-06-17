using Escaplanet.Ingame.Data.Common;

namespace Escaplanet.Ingame.Data.Player
{
    public interface IPlayerMoveEntity : IEntity
    {
        float MoveSpeed { get; }
        float Acceleration { get; }
        float MovementLerpAmount { get; }

        bool IsFlayingAway { get; set; }

        float RotateSpeed { get; }

        Vector2 Position { get; }
        float Rotation { get; }
        Vector2 Velocity { get; }

        void Move(Vector2 velocity);
        void Rotate(float angle);
    }
}