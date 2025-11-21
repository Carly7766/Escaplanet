using Escaplanet.Ingame.Core.Gimmick.GoalFlag;
using Escaplanet.Ingame.EntryPoint.Gimmick;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Gimmick
{
    public class GoalFlagLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GetComponent<IGoalFlagCore>());
            builder.RegisterEntryPoint<GoalFlagEntryPoint>();
        }
    }
}