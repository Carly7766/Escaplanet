using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public class AttractUpdateLogic : IAttractUpdateLogic
    {
        public void UpdateAttract(IReadonlyAttractableCore attractable)
        {
            foreach (var source in attractable.AffectingSources)
            {
                var sourceMass = source.SurfaceGravity * (source.Radius * source.Radius) / source.GravityConstant;

                var direction = source.Position - attractable.Position;
                var distance = direction.Magnitude();

                if (distance <= ScalarFloat.Zero) continue;

                var forceMagnitude = source.GravityConstant * (sourceMass * attractable.Mass) / (distance * distance);

                var force = direction.Normalize() * forceMagnitude;
                attractable.Attract(force);
            }
        }
    }
}