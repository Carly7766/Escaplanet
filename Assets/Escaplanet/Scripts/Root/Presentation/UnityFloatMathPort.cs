using Escaplanet.Root.Core.Common;
using UnityEngine;

namespace Escaplanet.Root.Presentation
{
    public class UnityFloatMathPort : IFloatMathPort
    {
        public float Epsilon => Mathf.Epsilon;

        public bool Approximately(float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        public float Clamp(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public float DeltaAngle(float current, float target)
        {
            return Mathf.DeltaAngle(current, target);
        }

        public float NormalizedAngle180(float angle)
        {
            angle = (angle + 180) % 360 - 180;
            if (angle < -180) angle += 360;
            return angle;
        }

        public float Lerp(float a, float b, float t)
        {
            return Mathf.Lerp(a, b, t);
        }

        public float Abs(float value)
        {
            return Mathf.Abs(value);
        }

        public float Min(float a, float b)
        {
            return Mathf.Min(a, b);
        }

        public float Max(float a, float b)
        {
            return Mathf.Max(a, b);
        }
    }
}