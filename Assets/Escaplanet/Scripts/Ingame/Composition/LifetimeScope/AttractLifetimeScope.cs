using System.Linq;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.EntryPoint.Attract;
using Escaplanet.Ingame.GameLogic.Attract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope
{
    public class AttractLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var attractSources = FindObjectsOfType<MonoBehaviour>().OfType<IAttractSourceCore>();
            foreach (var planetAttractSource in attractSources)
                builder.Register(_ => planetAttractSource, Lifetime.Scoped).AsSelf();

            var attractables = FindObjectsOfType<MonoBehaviour>().OfType<IAttractableCore>();
            foreach (var planetAttractSource in attractables)
                builder.Register(_ => planetAttractSource, Lifetime.Scoped).AsSelf();

            builder.Register<AttractUpdateLogic>(Lifetime.Singleton)
                .As<IAttractUpdateLogic>();
            builder.Register<RotateUpdateLogic>(Lifetime.Singleton)
                .As<IRotateUpdateLogic>();
            builder.Register<AttractAreaDetectionLogic>(Lifetime.Singleton)
                .As<IAttractAreaDetectionLogic>();

            builder.RegisterEntryPoint<AttractEntryPoint>(Lifetime.Singleton);
        }
    }
}