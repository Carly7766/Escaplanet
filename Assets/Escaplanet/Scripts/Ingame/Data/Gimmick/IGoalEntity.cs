using R3;

namespace Escaplanet.Ingame.Data.Gimmick
{
    public interface IGoalEntity : IEntity
    {
        Observable<Unit> OnGoalReached { get; }
    }
}