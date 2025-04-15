using System;
using System.Collections.Generic;
using Escaplanet.Ingame.Core.Planet;
using R3;

namespace Escaplanet.Ingame.Core.AttractSystem
{
    public class AttractInfo : IDisposable
    {
        public IPlanetAttractSourceEntity Source { get; }
        public HashSet<IAttractableEntity> Attractables { get; } = new();
        public CompositeDisposable Disposables { get; } = new();

        public AttractInfo(IPlanetAttractSourceEntity source)
        {
            Source = source;
        }

        public void Dispose()
        {
            Disposables.Dispose();
            Attractables.Clear();
        }
    }
}