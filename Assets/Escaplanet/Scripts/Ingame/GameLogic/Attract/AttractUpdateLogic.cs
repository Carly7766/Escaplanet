using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common.Service;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public class AttractUpdateLogic : IAttractUpdateLogic
    {
        public void UpdateAttract(IReadonlyAttractableCore attractable)
        {
            var epsilon2 = MathFService.Pow(ScalarFloat.Epsilon, ScalarFloat.Two);
            var totalForce = Vector2.Zero;

            foreach (var source in attractable.AffectingSources)
            {
                var mu = source.SurfaceGravity * (source.Radius * source.Radius); // = G*M

                var dir = source.Position - attractable.Position;
                var r2 = dir.SquareMagnitude();

                if (r2 < epsilon2) r2 = epsilon2;

                var invR = MathFService.InvSqrt(r2);
                var invR3 = MathFService.Pow(invR, new ScalarFloat(3f));

                var scale = (mu * attractable.Mass) * invR3;
                totalForce += dir * scale;
            }

            attractable.Attract(totalForce);
        }
    }
}