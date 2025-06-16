using System.Collections.Generic;
using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.EntityId;
using R3;
using UnityEngine;
using Vector2 = Escaplanet.Ingame.Data.Common.Vector2;

namespace Escaplanet.Ingame.Framework.Attract
{
    public class AttractableComponent : MonoBehaviour, IAttractableEntity
    {
        private readonly HashSet<IAttractSourceEntity> _affectingSources = new();
        private IEntityIdGenerator _entityIdGenerator;
        private readonly Subject<EntityId> _onDestroySubject = new();
        private Rigidbody2D _rigidbody2D;

        private Transform _transform;


        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            _onDestroySubject.OnNext(Id);
            _onDestroySubject.OnCompleted();
            Dispose();
        }

        public EntityId Id { get; private set; }

        public bool IsActive => isActiveAndEnabled;
        public bool IsDestroyed => !this;
        Observable<EntityId> IEntity.OnDestroy => _onDestroySubject;

        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public float Mass => _rigidbody2D.mass;
        public IReadOnlyCollection<IAttractSourceEntity> AffectingSources => _affectingSources;
        public IAttractSourceEntity NearestSource { get; set; }

        public void Attract(Vector2 force)
        {
            _rigidbody2D.AddForce(new UnityEngine.Vector2(force.X, force.Y));
        }

        public void AddAffectingSource(IAttractSourceEntity source)
        {
            _affectingSources.Add(source);
        }

        public void RemoveAffectingSource(IAttractSourceEntity source)
        {
            _affectingSources.Remove(source);
            if (NearestSource == source)
                NearestSource = null;
        }

        public void Initialize(IEntityIdGenerator entityIdGenerator)
        {
            _entityIdGenerator = entityIdGenerator;
            Id = entityIdGenerator.Generate();
        }

        public void Dispose()
        {
            _entityIdGenerator.Recycle(Id);
        }
    }
}