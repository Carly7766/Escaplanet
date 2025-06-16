using System;
using Escaplanet.Ingame.Data.EntityId;
using R3;

namespace Escaplanet.Ingame.Data
{
    public interface IEntity : IDisposable
    {
        EntityId.EntityId Id { get; }
        bool IsActive { get; }
        bool IsDestroyed { get; }
        Observable<EntityId.EntityId> OnDestroy { get; }

        void Initialize(IEntityIdGenerator entityIdGenerator);
    }
}