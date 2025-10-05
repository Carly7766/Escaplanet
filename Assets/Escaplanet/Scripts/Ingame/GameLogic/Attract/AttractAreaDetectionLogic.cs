using System.Linq;
using Escaplanet.Ingame.Core.Attract;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public class AttractAreaDetectionLogic : IAttractAreaDetectionLogic
    {
        public void OnSourceEnter(IAttractSourceCore source, IAttractableCore attractable)
        {
            source.AddAttractableInArea(attractable);
            attractable.AddAffectingSource(source);
        }

        public void OnSourceExit(IAttractSourceCore source, IAttractableCore attractable)
        {
            source.RemoveAttractableInArea(attractable);
            attractable.RemoveAffectingSource(source);
        }

        public void ExcludeDestroyedSources(IAttractableCore attractable)
        {
            var destroyedSources = attractable.AffectingSources.Where(s => s.IsDestroyed).ToList();

            foreach (var destroyedSource in destroyedSources)
            {
                attractable.RemoveAffectingSource(destroyedSource);
            }
        }
    }
}