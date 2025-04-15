using Escaplanet.Ingame.Core.AttractSystem;

namespace Escaplanet.Ingame.Core.Planet
{
    public interface IPlanetAttractSourceEntity : IAttractSourceEntity
    {
        float Mass { get; }
        float GravityConstant { get; }
    }
}