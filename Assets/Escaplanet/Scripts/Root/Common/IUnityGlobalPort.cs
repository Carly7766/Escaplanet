using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Root.Common
{
    public interface IUnityGlobalPort
    {
        ScalarFloat FixedDeltaTime { get; }
    }
}