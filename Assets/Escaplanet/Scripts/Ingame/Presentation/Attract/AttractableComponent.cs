using System.Collections.Generic;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common.ValueObject;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Attract
{
    public class AttractableComponent : MonoBehaviour, IAttractableCore
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        private readonly HashSet<IReadonlyAttractSourceCore> _affectingSources = new();


        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public ScalarFloat Mass => new(_rigidbody2D.mass);

        public IReadOnlyCollection<IReadonlyAttractSourceCore> AffectingSources => _affectingSources;
        public IReadonlyAttractSourceCore NearestSource { get; private set; }


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
    }
}