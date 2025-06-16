using System.Linq;
using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.System.Entity;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Ingame
{
    public class IngameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var entities = FindObjectsOfType<MonoBehaviour>().OfType<IEntity>();
            foreach (var entity in entities) builder.Register(_ => entity, Lifetime.Scoped).AsSelf();

            builder.Register<PooledEntityIdGenerator>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<EntityInitializer>();
        }
    }
}