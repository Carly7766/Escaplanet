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

        public void DetectNearestSource(IAttractableCore attractable)
        {
            attractable.SetNearestSource(
                attractable.AffectingSources
                    .OrderBy(s => (attractable.Position - s.Position).SquareMagnitude())
                    .FirstOrDefault()
            );
        }
    }
}