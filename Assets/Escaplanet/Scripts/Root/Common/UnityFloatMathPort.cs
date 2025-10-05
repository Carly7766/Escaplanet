using UnityEngine;

namespace Escaplanet.Root.Common
{
    public class UnityFloatMathPort : IFloatMathPort
    {
        public float Epsilon => Mathf.Epsilon;

        public bool Approximately(float a, float b) => Mathf.Approximately(a, b);
        public float Clamp(float value, float min, float max) => Mathf.Clamp(value, min, max);
        public float DeltaAngle(float current, float target) => Mathf.DeltaAngle(current, target);

        public float NormalizedAngle180(float angle)
        {
            angle = (angle + 180) % 360 - 180;
            if (angle < -180) angle += 360;
            return angle;
        }
    }
}