using System.Linq;
using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.Gimmick;
using Escaplanet.Ingame.System.Gimmick.Goal;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Gimmick
{
    public class GimmickLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var attractSources = FindObjectsOfType<MonoBehaviour>().OfType<IGoalEntity>();
            foreach (var planetAttractSource in attractSources)
                builder.Register(_ => planetAttractSource, Lifetime.Scoped).AsSelf();

            builder.Register<GoalGimmickSystem>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<GimmickEntryPoint>();
        }
    }
}