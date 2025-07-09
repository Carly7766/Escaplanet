using Escaplanet.Ingame.Data.Gimmick;
using Escaplanet.Ingame.System.Gimmick.Goal;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Gimmick
{
    public class GimmickEntryPoint : IStartable
    {
        private readonly IGoalEntity goalEntity;
        private readonly GoalGimmickSystem goalGimmickSystem;

        public GimmickEntryPoint(IGoalEntity goalEntity, GoalGimmickSystem goalGimmickSystem)
        {
            this.goalEntity = goalEntity;
            this.goalGimmickSystem = goalGimmickSystem;
        }

        public void Start()
        {
            goalGimmickSystem.RegisterGoalGimmick(goalEntity);
        }
    }
}