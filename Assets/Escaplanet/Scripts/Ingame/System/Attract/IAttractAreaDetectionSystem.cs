using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.EntityId;

namespace Escaplanet.Ingame.System.Attract
{
    public interface IAttractAreaDetectionSystem
    {
        void RegisterAttractSource(IAttractSourceEntity source);
        void UnregisterAttractSource(EntityId id);
        void RegisterAttractable(IAttractableEntity attractable);
        void UnregisterAttractable(EntityId id);
    }
}