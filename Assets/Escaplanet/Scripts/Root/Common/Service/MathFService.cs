using System;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Root.Common.Service
{
    public static class MathFService
    {
        public static ScalarFloat Abs(ScalarFloat value)
        {
            return new ScalarFloat(MathF.Abs(value.Value));
        }

        public static ScalarFloat Clamp(ScalarFloat value, ScalarFloat min, ScalarFloat max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static ScalarFloat Clamp01(ScalarFloat value)
        {
            return Clamp(value, ScalarFloat.Zero, ScalarFloat.One);
        }

        public static ScalarFloat Lerp(ScalarFloat a, ScalarFloat b, ScalarFloat t)
        {
            return a + (b - a) * Clamp01(t);
        }

        public static ScalarFloat Pow(ScalarFloat a, ScalarFloat b)
        {
            return new ScalarFloat(MathF.Pow(a.Value, b.Value));
        }

        public static ScalarFloat Sqrt(ScalarFloat value)
        {
            return new ScalarFloat(MathF.Sqrt(value.Value));
        }

        public static ScalarFloat InvSqrt(ScalarFloat value)
        {
            return new ScalarFloat(1.0f / MathF.Sqrt(value.Value));
        }

        public static ScalarFloat Atan2(ScalarFloat y, ScalarFloat x)
        {
            return new ScalarFloat(MathF.Atan2(y.Value, x.Value));
        }

        public static ScalarFloat Floor(ScalarFloat value)
        {
            return new ScalarFloat(MathF.Floor(value.Value));
        }

        public static ScalarFloat Sign(ScalarFloat value)
        {
            return new ScalarFloat(MathF.Sign(value.Value));
        }
    }
}