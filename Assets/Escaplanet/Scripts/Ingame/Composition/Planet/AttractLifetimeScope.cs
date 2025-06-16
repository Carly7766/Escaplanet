using System.Linq;
using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.EntityId;
using Escaplanet.Ingame.System.Attract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Planet
{
    public class AttractLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var attractSources = FindObjectsOfType<MonoBehaviour>().OfType<IAttractSourceEntity>();
            foreach (var planetAttractSource in attractSources)
                builder.Register(_ => planetAttractSource, Lifetime.Scoped).AsSelf();

            var attractables = FindObjectsOfType<MonoBehaviour>().OfType<IAttractableEntity>();
            foreach (var planetAttractSource in attractables)
                builder.Register(_ => planetAttractSource, Lifetime.Scoped).AsSelf();

            builder.Register<PlanetAttractAreaDetectionSystem>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlanetAttractSystem>(Lifetime.Scoped).AsImplementedInterfaces();

            builder.RegisterEntryPoint<AttractEntryPoint>(Lifetime.Scoped);

            builder.RegisterFactory<GameObject, Vector2, IAttractableEntity>(container =>
            {
                var entityIdGenerator = container.Resolve<IEntityIdGenerator>();
                var attractAreaDetectionSystem = container.Resolve<IAttractAreaDetectionSystem>();
                return (bombPrefab, spawnPosition) =>
                {
                    var entity = container.Instantiate(bombPrefab, spawnPosition, Quaternion.identity)
                        .GetComponent<IAttractableEntity>();
                    entity.Initialize(entityIdGenerator);
                    attractAreaDetectionSystem.RegisterAttractable(entity);
                    return entity;
                };
            }, Lifetime.Scoped);
        }
    }
}