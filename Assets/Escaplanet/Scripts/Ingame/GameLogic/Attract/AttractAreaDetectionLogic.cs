using System.Linq;
using Escaplanet.Ingame.Core.Attract;
using UnityEngine;

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
    }
}