using System.Collections.Generic;
using System.Linq;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common.ValueObject;
using R3;
using R3.Triggers;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Attract
{
    public class AttractableComponent : MonoBehaviour, IAttractableCore
    {
        private Transform _transform;
        protected Rigidbody2D Rigidbody2D;
        private Collider2D _collider2D;

        private readonly HashSet<IReadonlyAttractSourceCore> _affectingSources = new();


        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public ScalarFloat Mass => new(Rigidbody2D.mass);

        public IReadOnlyCollection<IReadonlyAttractSourceCore> AffectingSources => _affectingSources;

        public IReadonlyAttractSourceCore NearestSource
        {
            get
            {
                return _affectingSources
                    .OrderBy(s => (Position - s.Position).SquareMagnitude())
                    .FirstOrDefault();
            }
        }

        public Observable<IAttractSourceCore> OnEnterAttractArea { get; private set; }
        public Observable<IAttractSourceCore> OnExitAttractArea { get; private set; }


        private void Awake()
        {
            _transform = transform;
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();

            OnEnterAttractArea = _collider2D
                .OnTriggerEnter2DAsObservable()
                .Select(o => o.GetComponentInParent<IAttractSourceCore>())
                .Where(a => a != null)
                .TakeUntil(destroyCancellationToken);

            OnExitAttractArea = _collider2D
                .OnTriggerExit2DAsObservable()
                .Select(o => o.GetComponentInParent<IAttractSourceCore>())
                .Where(a => a != null)
                .TakeUntil(destroyCancellationToken);
        }


        public void Attract(Vector2 force)
        {
            Rigidbody2D.AddForce(new UnityEngine.Vector2(force.X, force.Y));
        }

        public void AddAffectingSource(IReadonlyAttractSourceCore source)
        {
            _affectingSources.Add(source);
        }

        public void RemoveAffectingSource(IReadonlyAttractSourceCore source)
        {
            _affectingSources.Remove(source);
        }
    }
}