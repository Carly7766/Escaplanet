using Escaplanet.Ingame.Core.Attract;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.Attract
{
    public class RotateAttractableComponent : AttractableComponent, IRotateAttractableCore
    {
        [SerializeField] private float angularVelocity;
        [SerializeField] private float smoothTime = 0.1f;
        [SerializeField] private float maxRotateSpeed = 360.0f;


        public float Rotation => Rigidbody2D.rotation;
        public float PreviousTargetRotation { get; set; }

        public float AngularVelocity
        {
            get => angularVelocity;
            set => angularVelocity = value;
        }

        public float SmoothTime => smoothTime;
        public float MaxRotateSpeed => maxRotateSpeed;

        public void Rotate(float angle)
        {
            Rigidbody2D.MoveRotation(angle);
        }
    }
}