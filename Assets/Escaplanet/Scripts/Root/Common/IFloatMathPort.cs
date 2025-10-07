namespace Escaplanet.Root.Common
{
    public interface IFloatMathPort
    {
        float Epsilon { get; }
        bool Approximately(float a, float b);
        float Clamp(float value, float min, float max);
        float DeltaAngle(float current, float target);
        float NormalizedAngle180(float angle);
        float Lerp(float a, float b, float t);
        float Abs(float value);
    }
}