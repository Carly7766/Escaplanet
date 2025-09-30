using Escaplanet.Ingame.Core.Attract;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public interface IAttractUpdateLogic
    {
        void UpdateAttract(IReadonlyAttractSourceCore source);
    }
}