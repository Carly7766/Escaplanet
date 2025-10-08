namespace Escaplanet.Root.Common
{
    public interface IGlobalValuePort
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
    }
}