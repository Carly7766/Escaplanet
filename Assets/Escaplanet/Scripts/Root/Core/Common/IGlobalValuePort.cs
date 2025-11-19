namespace Escaplanet.Root.Core.Common
{
    public interface IGlobalValuePort
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
    }
}