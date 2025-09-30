using System.Collections.Generic;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common.ValueObject;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Attract
{
    public class RotateAttractableComponent : MonoBehaviour, IRotateAttractableCore
    {
        [SerializeField] private float angularVelocity = 0.0f;
        [SerializeField] private float smoothTime = 0.1f;
        [SerializeField] private float maxRotateSpeed = 360.0f;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        private readonly HashSet<IReadonlyAttractSourceCore> _affectingSources = new();


        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public ScalarFloat Mass => new(_rigidbody2D.mass);

        public IReadOnlyCollection<IReadonlyAttractSourceCore> AffectingSources => _affectingSources;
        public IReadonlyAttractSourceCore NearestSource { get; private set; }


        public ScalarFloat Rotation => new(_rigidbody2D.rotation);
        public ScalarFloat PreviousTargetRotation { get; set; }

        public ScalarFloat AngularVelocity
        {
            get => new(angularVelocity);
            set => angularVelocity = value.Value;
        }

        public ScalarFloat SmoothTime => new(smoothTime);
        public ScalarFloat MaxRotateSpeed => new(maxRotateSpeed);


        private void Awake()
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        public void Attract(Vector2 force)
        {
            _rigidbody2D.AddForce(new UnityEngine.Vector2(force.X, force.Y));
        }

        public void AddAffectingSource(IReadonlyAttractSourceCore source)
        {
            _affectingSources.Add(source);
        }

        public void RemoveAffectingSource(IReadonlyAttractSourceCore source)
        {
            _affectingSources.Remove(source);
        }

        public void SetNearestSource(IReadonlyAttractSourceCore source)
        {
            NearestSource = source;
        }

        public void Rotate(ScalarFloat angle)
        {
            _rigidbody2D.MoveRotation(angle.Value);
        }
    }
}