using System;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Core.Common;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public class RotateUpdateLogic : IRotateUpdateLogic
    {
        private readonly IFloatMathPort _floatMathPort;
        private readonly IGlobalValuePort _globalValuePort;

        public RotateUpdateLogic(IGlobalValuePort globalValuePort, IFloatMathPort floatMathPort)
        {
            _globalValuePort = globalValuePort;
            _floatMathPort = floatMathPort;
        }

        public void UpdateRotate(IRotateAttractableCore attractable)
        {
            if (attractable.NearestSource == null) return;

            var direction = (attractable.NearestSource.Position - attractable.Position).Normalize();
            var angle = MathF.Atan2(direction.Y, direction.X) * (360 / (MathF.PI * 2));
            var targetAngle = angle + 90f;

            var attractableAngularVelocity = attractable.AngularVelocity;

            var newAngle = SlerpAngle(attractable.Rotation, targetAngle, attractable.PreviousTargetRotation,
                ref attractableAngularVelocity, attractable.SmoothTime, attractable.MaxRotateSpeed,
                _globalValuePort.FixedDeltaTime);

            attractable.Rotate(newAngle);
            attractable.PreviousTargetRotation = targetAngle;
            attractable.AngularVelocity = attractableAngularVelocity;
        }

        private float SlerpAngle(float current, float target, float previous,
            ref float angularVelocity,
            float smoothTime, float maxRotateSpeed, float deltaTime)
        {
            var projectedTarget = current + _floatMathPort.DeltaAngle(current, target);
            var projectedPrev = current + _floatMathPort.DeltaAngle(current, previous);

            var passed =
                MathF.Abs(projectedTarget - current) < _floatMathPort.Epsilon ||
                (projectedPrev < current && current < projectedTarget) ||
                (projectedPrev > current && current > projectedTarget);

            if (passed)
            {
                angularVelocity = 0f;
                return _floatMathPort.NormalizedAngle180(current);
            }

            var omega = 2f / smoothTime;
            var x = omega * deltaTime;
            var exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);

            var change = _floatMathPort.DeltaAngle(current, projectedTarget) * -1f;
            var maxChange = maxRotateSpeed * smoothTime;
            change = _floatMathPort.Clamp(change, -maxChange, maxChange);

            var tempTarget = current - change;

            var temp = (angularVelocity + omega * change) * deltaTime;
            angularVelocity = (angularVelocity - omega * temp) * exp;

            var output = tempTarget + (change + temp) * exp;

            if (_floatMathPort.Approximately(angularVelocity, 0f))
                angularVelocity = 0f;

            var overshot = projectedTarget > current ? output > projectedTarget : output < projectedTarget;
            if (overshot)
            {
                angularVelocity = 0f;
                return _floatMathPort.NormalizedAngle180(projectedTarget);
            }

            return _floatMathPort.NormalizedAngle180(output);
        }
    }
}