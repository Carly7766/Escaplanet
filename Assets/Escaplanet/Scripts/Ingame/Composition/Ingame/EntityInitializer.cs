using System.Collections.Generic;
using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.Data.EntityId;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Ingame
{
    public class EntityInitializer : IInitializable
    {
        private readonly IEnumerable<IEntity> entities;
        private readonly IEntityIdGenerator entityIdGenerator;

        public EntityInitializer(IEnumerable<IEntity> entities, IEntityIdGenerator entityIdGenerator)
        {
            this.entities = entities;
            this.entityIdGenerator = entityIdGenerator;
        }

        public void Initialize()
        {
            foreach (var entity in entities) entity.Initialize(entityIdGenerator);
        }
    }
}