using System;
using System.Collections.Generic;
using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.Data.EntityId;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Ingame
{
    public class EntityIdScheduler : IStartable, IDisposable
    {
        private CompositeDisposable _disposables = new();
        private readonly IEnumerable<IEntity> entities;
        private readonly IEntityIdGenerator entityIdGenerator;

        public EntityIdScheduler(IEnumerable<IEntity> entities, IEntityIdGenerator entityIdGenerator)
        {
            this.entities = entities;
            this.entityIdGenerator = entityIdGenerator;
        }

        public void Start()
        {
            foreach (var entity in entities)
            {
                AttachAndScheduleRecycleEntityId(entity);
            }
        }

        public void AttachAndScheduleRecycleEntityId(IEntity entity)
        {
            entity.Initialize(entityIdGenerator.Generate());
            ScheduleRecycleEntityId(entity);
        }

        private void ScheduleRecycleEntityId(IEntity entity)
        {
            entity.OnDestroy.Subscribe(id => entityIdGenerator.Recycle(id)).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}