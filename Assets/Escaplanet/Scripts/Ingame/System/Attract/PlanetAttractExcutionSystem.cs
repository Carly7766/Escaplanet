using System;
using System.Collections.Generic;
using System.Linq;
using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.EntityId;
using R3;

namespace Escaplanet.Ingame.System.Attract
{
    public class PlanetAttractSystem : IAttractSystem, IDisposable
    {
        private readonly Dictionary<EntityId, (IReadOnlyAttractSourceEntity source, CompositeDisposable disposable)>
            _sourceEntities =
                new();

        public void RegisterSourceEntity(IReadOnlyAttractSourceEntity source)
        {
            var disposable = new CompositeDisposable();
            source.OnDestroy.Subscribe(_ => UnregisterSourceEntity(source.Id)).AddTo(disposable);

            _sourceEntities.Add(source.Id, (source, disposable));
        }

        public void UnregisterSourceEntity(EntityId entityId)
        {
            _sourceEntities[entityId].disposable.Dispose();
            _sourceEntities.Remove(entityId);
        }

        public void SimulateAttract()
        {
            foreach (var source in _sourceEntities.Values.Select(pair => pair.Item1))
            {
                var sourceMass = source.SurfaceGravity * MathF.Pow(source.Radius, 2) / source.GravityConstant;

                foreach (var attractable in source.AttractablesInArea)
                {
                    var direction = source.Position.Subtract(attractable.Position);
                    var distance = direction.Magnitude();

                    if (distance <= 0) continue;

                    var forceMagnitude =
                        source.GravityConstant * (sourceMass * attractable.Mass) / (distance * distance);

                    var force = direction.Normalize().Multiply(forceMagnitude);
                    attractable.Attract(force);
                }
            }
        }

        public void Dispose()
        {
            _sourceEntities
                .Select(pair => pair.Value.disposable)
                .ToList()
                .ForEach(disposable => disposable.Dispose());

            _sourceEntities.Clear();
        }
    }
}