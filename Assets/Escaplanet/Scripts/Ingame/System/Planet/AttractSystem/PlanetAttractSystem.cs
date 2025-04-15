using System;
using System.Collections.Generic;
using Escaplanet.Ingame.Core.AttractSystem;
using Escaplanet.Ingame.Core.Planet;
using R3;
using System.Linq;
using UnityEngine;

namespace Escaplanet.Ingame.System.Planet.AttractSystem
{
    public class PlanetAttractSystem : IPlanetAttractSystem, IDisposable
    {
        private readonly List<AttractInfo> _attractionInfos = new();

        public void Register(IPlanetAttractSourceEntity entity)
        {
            if (_attractionInfos.Any(info => ReferenceEquals(info.Source, entity))) return;

            var newInfo = new AttractInfo(entity);
            _attractionInfos.Add(newInfo);

            entity.OnEnterAttractArea
                .Where(target => target != null)
                .Subscribe(target => newInfo.Attractables.Add(target))
                .AddTo(newInfo.Disposables);

            entity.OnExitAttractArea
                .Where(target => target != null)
                .Subscribe(target => newInfo.Attractables.Remove(target))
                .AddTo(newInfo.Disposables);
        }

        public void Unregister(IPlanetAttractSourceEntity entity)
        {
            var index = _attractionInfos.FindIndex(info => ReferenceEquals(info.Source, entity));
            if (index < 0) return;

            var infoToRemove = _attractionInfos[index];
            infoToRemove.Dispose();
            _attractionInfos.RemoveAt(index);
        }

        public void Execute()
        {
            for (int i = _attractionInfos.Count - 1; i >= 0; i--)
            {
                var info = _attractionInfos[i];
                var source = info.Source;

                if (source.IsNullEntity)
                {
                    info.Dispose();
                    _attractionInfos.RemoveAt(i);
                }
            }
        }

        public void FixedExecute()
        {
            for (int i = _attractionInfos.Count - 1; i >= 0; i--)
            {
                var info = _attractionInfos[i];
                var source = info.Source;

                foreach (var target in info.Attractables)
                {
                    var direction = source.Position.Subtract(target.Position);
                    var distance = direction.Magnitude();

                    if (distance <= 0) continue;

                    var forceMagnitude = source.GravityConstant * (source.Mass * target.Mass) / (distance * distance);
                    var force = direction.Normalize().Multiply(forceMagnitude);

                    target.Attract(force);
                }
            }
        }

        public void Dispose()
        {
            foreach (var info in _attractionInfos)
            {
                info.Dispose();
            }

            _attractionInfos.Clear();
        }
    }
}