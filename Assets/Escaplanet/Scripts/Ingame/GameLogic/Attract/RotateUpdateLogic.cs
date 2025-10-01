using System;
using System.Linq;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.ValueObject;
using UnityEngine;

namespace Escaplanet.Ingame.GameLogic.Attract
{
    public class RotateUpdateLogic : IRotateUpdateLogic
    {
        private readonly IUnityGlobalPort _unityGlobalPort;

        public RotateUpdateLogic(IUnityGlobalPort unityGlobalPort)
        {
            _unityGlobalPort = unityGlobalPort;
        }

        public void UpdateRotate(IRotateAttractableCore attractable)
        {
            if (attractable.NearestSource == null) return;

            var direction = (attractable.NearestSource.Position - attractable.Position).Normalize();
            var angle = new ScalarFloat(MathF.Atan2(direction.Y, direction.X) * Mathf.Rad2Deg);
            var targetAngle = angle + new ScalarFloat(90f);

            var attractableAngularVelocity = attractable.AngularVelocity;

            var newAngle = SlerpAngle(attractable.Rotation, targetAngle, attractable.PreviousTargetRotation,
                ref attractableAngularVelocity, attractable.SmoothTime, attractable.MaxRotateSpeed,
                _unityGlobalPort.FixedDeltaTime);

            attractable.Rotate(newAngle);
            attractable.PreviousTargetRotation = targetAngle;
            attractable.AngularVelocity = attractableAngularVelocity;
        }

        private static ScalarFloat WrapNeg180To180(ScalarFloat degrees)
        {
            var a = ((degrees + new ScalarFloat(180f)) % new ScalarFloat(360f) + new ScalarFloat(360f)) %
                new ScalarFloat(360f) - new ScalarFloat(180);
            if (ScalarFloat.Abs(a) < ScalarFloat.Epsilon) a = ScalarFloat.Zero;
            return a;
        }

        private ScalarFloat DeltaAngle(ScalarFloat current, ScalarFloat target)
        {
            var delta = WrapNeg180To180(target - current);
            return delta;
        }

        private ScalarFloat SlerpAngle(ScalarFloat current, ScalarFloat target, ScalarFloat previous,
            ref ScalarFloat angularVelocity,
            ScalarFloat smoothTime, ScalarFloat maxRotateSpeed, ScalarFloat deltaTime)
        {
            var projTarget = current + DeltaAngle(current, target);
            var projPrev = current + DeltaAngle(current, previous);

            var passedThrough =
                projTarget == current ||
                (projPrev < current && current < projTarget) ||
                (projPrev > current && current > projTarget);

            if (passedThrough)
            {
                angularVelocity = ScalarFloat.Zero;
                return WrapNeg180To180(current);
            }

            var omega = new ScalarFloat(2f) / smoothTime;
            var x = omega * deltaTime;
            var exp = new ScalarFloat(1f) / (new ScalarFloat(1f) + x + new ScalarFloat(0.48f) * x * x +
                                             new ScalarFloat(0.235f) * x * x * x);

            var change = current - target;
            var originalTo = target;

            var maxChange = maxRotateSpeed * smoothTime;
            change = ScalarFloat.Clamp(change, -maxChange, maxChange);
            target = current - change;

            var temp = (angularVelocity + omega * change) * deltaTime;
            angularVelocity = (angularVelocity - omega * temp) * exp;

            var output = target + (change + temp) * exp;

            if (ScalarFloat.Abs(angularVelocity) < ScalarFloat.Epsilon) angularVelocity = ScalarFloat.Zero;

            var overshot = (projTarget > current) ? (output > projTarget) : (output < projTarget);
            if (overshot)
            {
                angularVelocity = ScalarFloat.Zero;
                return projTarget;
            }

            return output;
        }
    }
}