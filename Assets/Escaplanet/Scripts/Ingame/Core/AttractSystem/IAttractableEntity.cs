using Escaplanet.Ingame.Core.Common;

namespace Escaplanet.Ingame.Core.AttractSystem
{
    public interface IAttractableEntity : IEntity
    {
        Vector2 Position { get; }
        float Mass { get; }

        void Attract(Vector2 force);
    }
}