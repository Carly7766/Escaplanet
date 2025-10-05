using System;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public class AttractUpdateLogic : IAttractUpdateLogic
    {
        private IFloatMathPort _floatMathPort;

        public AttractUpdateLogic(IFloatMathPort floatMathPort)
        {
            _floatMathPort = floatMathPort;
        }

        public void UpdateAttract(IReadonlyAttractableCore attractable)
        {
            var totalForce = Vector2.Zero;
            foreach (var source in attractable.AffectingSources)
            {
                var mu = source.SurfaceGravity * (source.Radius * source.Radius);

                var dir = source.Position - attractable.Position;
                var r2 = dir.SquareMagnitude();

                if (r2 < _floatMathPort.Epsilon)
                    r2 = _floatMathPort.Epsilon;

                var invR = 1.0f / MathF.Sqrt(r2);
                var invR3 = MathF.Pow(invR, 3);

                var scale = (mu * attractable.Mass) * invR3;
                totalForce += dir * scale;
            }

            attractable.Attract(totalForce);
        }
    }
}