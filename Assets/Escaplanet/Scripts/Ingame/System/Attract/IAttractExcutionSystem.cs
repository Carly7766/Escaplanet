using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.EntityId;

namespace Escaplanet.Ingame.System.Attract
{
    public interface IAttractSystem
    {
        void RegisterSourceEntity(IReadOnlyAttractSourceEntity source);

        void UnregisterSourceEntity(EntityId entityId);

        void SimulateAttract();
    }
}