namespace Escaplanet.Root.Common
{
    public interface IFloatMathPort
    {
        float Epsilon { get; }
        bool Approximately(float a, float b);
        float Clamp(float value, float min, float max);
        float DeltaAngle(float current, float target);
        float NormalizedAngle180(float angle);
    }
}