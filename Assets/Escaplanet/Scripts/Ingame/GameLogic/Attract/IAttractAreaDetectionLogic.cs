using Escaplanet.Ingame.Core.Attract;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public interface IAttractAreaDetectionLogic
    {
        void OnSourceEnter(IAttractSourceCore source, IAttractableCore attractable);
        void OnSourceExit(IAttractSourceCore source, IAttractableCore attractable);
        void ExcludeDestroyedSources(IAttractableCore attractable);
    }
}