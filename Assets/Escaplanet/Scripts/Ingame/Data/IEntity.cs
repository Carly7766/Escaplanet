using System;
using Escaplanet.Ingame.Data.EntityId;
using R3;

namespace Escaplanet.Ingame.Data
{
    public interface IEntity
    {
        EntityId.EntityId Id { get; }
        bool IsActive { get; }
        bool IsDestroyed { get; }
        Observable<EntityId.EntityId> OnDestroy { get; }

        void Initialize(EntityId.EntityId id);
    }
}