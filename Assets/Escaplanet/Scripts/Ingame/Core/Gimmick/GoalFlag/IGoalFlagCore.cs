using R3;

namespace Escaplanet.Ingame.Core.Gimmick.GoalFlag
{
    public interface IGoalFlagCore
    {
        Observable<Unit> OnGoalReached { get; }
    }
}