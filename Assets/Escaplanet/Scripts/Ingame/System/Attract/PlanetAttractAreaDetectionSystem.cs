using System;
using System.Collections.Generic;
using System.Linq;
using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.EntityId;
using R3;
using UnityEngine;

namespace Escaplanet.Ingame.System.Attract
{
    public class PlanetAttractAreaDetectionSystem : IAttractAreaDetectionSystem, IDisposable
    {
        private readonly Dictionary<EntityId, (IAttractableEntity attractable, CompositeDisposable disposable)>
            _attractables =
                new();

        private readonly Dictionary<EntityId, (IAttractSourceEntity source, CompositeDisposable disposable)>
            _attractSources =
                new();

        public void RegisterAttractSource(IAttractSourceEntity source)
        {
            var disposable = new CompositeDisposable();

            source.OnDestroy
                .Subscribe(_ => UnregisterAttractSource(source.Id))
                .AddTo(disposable);

            source.OnEnterAttractArea
                .Subscribe(a => { OnSourceEnter(source, a); })
                .AddTo(disposable);

            source.OnExitAttractArea
                .Subscribe(a => OnSourceExit(source, a))
                .AddTo(disposable);

            _attractSources.Add(source.Id, (source, disposable));
        }

        public void UnregisterAttractSource(EntityId id)
        {
            _attractSources[id].disposable.Dispose();
            _attractSources.Remove(id);
        }

        public void RegisterAttractable(IAttractableEntity attractable)
        {
            var disposable = new CompositeDisposable();

            attractable.OnDestroy
                .Subscribe(_ => UnregisterAttractable(attractable.Id))
                .AddTo(disposable);

            _attractables.Add(attractable.Id, (attractable, disposable));
        }

        public void UnregisterAttractable(EntityId id)
        {
            _attractables[id].disposable.Dispose();
            _attractables.Remove(id);
        }

        public void Dispose()
        {
            _attractSources
                .Select(pair => pair.Value.disposable)
                .ToList()
                .ForEach(disposable => disposable.Dispose());

            _attractables
                .Select(pair => pair.Value.disposable)
                .ToList()
                .ForEach(disposable => disposable.Dispose());

            _attractSources.Clear();
            _attractables.Clear();
        }

        private void OnSourceEnter(IAttractSourceEntity source, IAttractableEntity attractable)
        {
            source.AddAttractableInArea(attractable);
            attractable.AddAffectingSource(source);
            UpdateNearest(attractable, source);
        }

        private void OnSourceExit(IAttractSourceEntity source, IAttractableEntity attractable)
        {
            source.RemoveAttractableFromArea(attractable);
            attractable.RemoveAffectingSource(source);
            if (attractable.NearestSource == source) RecalculateNearest(attractable);
        }

        private void UpdateNearest(IAttractableEntity attractable, IAttractSourceEntity newSource)
        {
            var current = attractable.NearestSource;
            var dNew = newSource.Position.Subtract(attractable.Position).Magnitude();
            var dCur = current != null
                ? current.Position.Subtract(attractable.Position).Magnitude()
                : float.MaxValue;
            if (dNew < dCur)
                attractable.NearestSource = newSource;
        }

        private void RecalculateNearest(IAttractableEntity attractable)
        {
            var next = attractable.AffectingSources
                .OrderBy(s => s.Position.Subtract(attractable.Position).Magnitude())
                .FirstOrDefault();
            attractable.NearestSource = next;
        }
    }
}