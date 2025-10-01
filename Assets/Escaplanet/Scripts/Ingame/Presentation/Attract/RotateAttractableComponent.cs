using System.Collections.Generic;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common.ValueObject;
using R3;
using R3.Triggers;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Attract
{
    public class RotateAttractableComponent : AttractableComponent, IRotateAttractableCore
    {
        [SerializeField] private float angularVelocity = 0.0f;
        [SerializeField] private float smoothTime = 0.1f;
        [SerializeField] private float maxRotateSpeed = 360.0f;


        public ScalarFloat Rotation => new(base.Rigidbody2D.rotation);
        public ScalarFloat PreviousTargetRotation { get; set; }

        public ScalarFloat AngularVelocity
        {
            get => new(angularVelocity);
            set => angularVelocity = value.Value;
        }

        public ScalarFloat SmoothTime => new(smoothTime);
        public ScalarFloat MaxRotateSpeed => new(maxRotateSpeed);

        public void Rotate(ScalarFloat angle)
        {
            Rigidbody2D.MoveRotation(angle.Value);
        }
    }
}