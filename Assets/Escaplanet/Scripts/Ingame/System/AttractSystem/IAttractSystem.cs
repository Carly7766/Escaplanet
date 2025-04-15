using Escaplanet.Ingame.Core.AttractSystem;

namespace Escaplanet.Ingame.System.AttractSystem
{
    public interface IAttractSystem<in TAttractSourceEntity> : IFixedExecuteSystem<TAttractSourceEntity>
        where TAttractSourceEntity : IAttractSourceEntity
    {
    }
}