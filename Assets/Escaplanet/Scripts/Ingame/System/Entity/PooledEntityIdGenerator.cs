using System.Collections.Generic;
using Escaplanet.Ingame.Data.EntityId;

namespace Escaplanet.Ingame.System.Entity
{
    public sealed class PooledEntityIdGenerator : IEntityIdGenerator
    {
        private readonly Dictionary<ushort, byte> generationHistory = new();
        private readonly Stack<ushort> unusedIdPool = new();
        private ushort _nextIndex;

        public EntityId Generate()
        {
            var index = unusedIdPool.TryPop(out var reused) ? reused : _nextIndex++;

            if (!generationHistory.TryGetValue(index, out var generation))
            {
                generation = 0;
                generationHistory[index] = generation;
            }

            return new EntityId(index, generation);
        }

        public void Recycle(EntityId id)
        {
            if (!id.IsValid) return;
            unchecked
            {
                generationHistory[id.Index]++;
            }

            unusedIdPool.Push(id.Index);
        }

        public bool IsAlive(EntityId id)
        {
            return id.IsValid &&
                   generationHistory.TryGetValue(id.Index, out var generation) &&
                   generation == id.Generation;
        }
    }
}