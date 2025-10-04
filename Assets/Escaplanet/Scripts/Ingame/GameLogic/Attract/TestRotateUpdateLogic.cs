using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.Service;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public class TestRotateUpdateLogic : IRotateUpdateLogic
    {
        private readonly IUnityGlobalPort _unityGlobalPort;

        public TestRotateUpdateLogic(IUnityGlobalPort unityGlobalPort)
        {
            _unityGlobalPort = unityGlobalPort;
        }

        public void UpdateRotate(IRotateAttractableCore attractable)
        {
            if (attractable.NearestSource == null) return;

            var toSrc = attractable.NearestSource.Position - attractable.Position;

            if (toSrc.SquareMagnitude() <= ScalarFloat.Epsilon) return;

            var targetAngle = MathFService.Atan2(toSrc.Y, toSrc.X) * ScalarFloat.Rad2Deg + new ScalarFloat(90f);
            var nextAngle = MoveTowardsAngle(
                attractable.Rotation,
                targetAngle,
                attractable.MaxRotateSpeed * _unityGlobalPort.FixedDeltaTime
            );
            attractable.Rotate(nextAngle);
        }

        public static ScalarFloat MoveTowardsAngle(ScalarFloat current, ScalarFloat target, ScalarFloat maxDelta)
        {
            maxDelta = MathFService.Abs(maxDelta);
            var delta = DeltaAngle(current, target);
            if (MathFService.Abs(delta) <= maxDelta) return target;
            return current + MathFService.Sign(delta) * maxDelta;
        }

        public static ScalarFloat DeltaAngle(ScalarFloat current, ScalarFloat target)
        {
            var delta = Repeat(target - current, new ScalarFloat(360f));
            if (delta > new ScalarFloat(180f)) delta -= new ScalarFloat(360f);
            return delta;
        }

        public static ScalarFloat Repeat(ScalarFloat t, ScalarFloat length)
        {
            return t - MathFService.Floor(t / length) * length;
        }
    }
}