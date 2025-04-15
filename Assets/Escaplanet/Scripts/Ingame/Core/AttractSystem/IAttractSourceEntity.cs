using Escaplanet.Ingame.Core.Common;
using R3;

namespace Escaplanet.Ingame.Core.AttractSystem
{
    public interface IAttractSourceEntity : IEntity
    {
        Vector2 Position { get; }
        Observable<IAttractableEntity> OnEnterAttractArea { get; }
        Observable<IAttractableEntity> OnExitAttractArea { get; }
    }
}