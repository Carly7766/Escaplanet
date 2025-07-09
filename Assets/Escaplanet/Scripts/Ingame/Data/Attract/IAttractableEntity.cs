using System.Collections.Generic;
using Escaplanet.Ingame.Data.Common;

namespace Escaplanet.Ingame.Data.Attract
{
    public interface IReadOnlyAttractableEntity : IEntity
    {
        Vector2 Position { get; }
        float Mass { get; }


        IReadOnlyCollection<IAttractSourceEntity> AffectingSources { get; }
        IAttractSourceEntity NearestSource { get; set; }

        void Attract(Vector2 force);
    }

    public interface IAttractableEntity : IReadOnlyAttractableEntity
    {
        void AddAffectingSource(IAttractSourceEntity source);
        void RemoveAffectingSource(IAttractSourceEntity source);
    }
}