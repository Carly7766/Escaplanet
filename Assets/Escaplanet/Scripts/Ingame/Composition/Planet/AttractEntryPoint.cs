using System.Collections.Generic;
using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.System.Attract;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Planet
{
    public class AttractEntryPoint : IStartable, IFixedTickable
    {
        private readonly IEnumerable<IAttractableEntity> attractables;
        private readonly IAttractAreaDetectionSystem attractAreaDetectionSystem;
        private readonly IEnumerable<IAttractSourceEntity> attractSources;
        private readonly IAttractSystem attractSystem;

        private AttractEntryPoint(
            IEnumerable<IAttractSourceEntity> attractSources,
            IEnumerable<IAttractableEntity> attractables,
            IAttractSystem attractSystem,
            IAttractAreaDetectionSystem attractAreaDetectionSystem)
        {
            this.attractSources = attractSources;
            this.attractables = attractables;
            this.attractSystem = attractSystem;
            this.attractAreaDetectionSystem = attractAreaDetectionSystem;
        }

        public void FixedTick()
        {
            attractSystem.SimulateAttract();
        }

        public void Start()
        {
            foreach (var attractSource in attractSources)
            {
                attractSystem.RegisterSourceEntity(attractSource);
                attractAreaDetectionSystem.RegisterAttractSource(attractSource);
            }

            foreach (var attractable in attractables) attractAreaDetectionSystem.RegisterAttractable(attractable);
        }
    }
}