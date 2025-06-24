using System.Collections.Generic;
using Escaplanet.Ingame.Data.Common;
using R3;

namespace Escaplanet.Ingame.Data.Attract
{
    public interface IReadOnlyAttractSourceEntity : IEntity
    {
        Vector2 Position { get; }

        float GravityConstant { get; }
        float SurfaceGravity { get; }
        float Radius { get; }

        IReadOnlyCollection<IAttractableEntity> AttractablesInArea { get; }

        Observable<Unit> OnUpdateAttractArea { get; }
        Observable<IAttractableEntity> OnEnterAttractArea { get; }
        Observable<IAttractableEntity> OnExitAttractArea { get; }
    }

    public interface IAttractSourceEntity : IReadOnlyAttractSourceEntity
    {
        void AddAttractableInArea(IAttractableEntity attractable);
        void RemoveAttractableFromArea(IAttractableEntity attractable);
    }
}