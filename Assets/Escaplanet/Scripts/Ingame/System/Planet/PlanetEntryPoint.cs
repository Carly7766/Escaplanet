using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core.Planet;
using Escaplanet.Ingame.System.Planet.AttractSystem;
using VContainer.Unity;

namespace Escaplanet.Ingame.System.Planet
{
    public class PlanetEntryPoint : IStartable, ITickable, IFixedTickable
    {
        private readonly IEnumerable<IPlanetAttractSourceEntity> _attractSources;
        private readonly IPlanetAttractSystem _planetAttractSystem;

        public PlanetEntryPoint(IEnumerable<IPlanetAttractSourceEntity> attractSources,
            IPlanetAttractSystem planetAttractSystem)
        {
            _attractSources = attractSources;
            _planetAttractSystem = planetAttractSystem;
        }

        public void Start()
        {
            foreach (var source in _attractSources)
            {
                _planetAttractSystem.Register(source);
            }
        }

        public void Tick()
        {
            _planetAttractSystem.Execute();
        }

        public void FixedTick()
        {
            _planetAttractSystem.FixedExecute();
        }
    }
}