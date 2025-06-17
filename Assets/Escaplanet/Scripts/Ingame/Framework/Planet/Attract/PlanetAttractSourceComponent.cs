using System.Collections.Generic;
using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.EntityId;
using R3;
using R3.Triggers;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;
using Vector2 = Escaplanet.Ingame.Data.Common.Vector2;

namespace Escaplanet.Ingame.Framework.Planet.Attract
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class PlanetAttractSourceComponent : MonoBehaviour, IAttractSourceEntity
    {
        [SerializeField] private float gravityConstant = 6.67430e-11f;

        [SerializeField] private float surfaceGravity = 1.0f;

        private readonly HashSet<IAttractableEntity> _attractablesInArea = new();
        private CircleCollider2D _attractAreaCollider;

        private readonly Subject<EntityId> _onDestroySubject = new();
        private CircleCollider2D _planetCollider;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _planetCollider = GetComponent<CircleCollider2D>();
            _attractAreaCollider = _transform.GetChild(0).GetComponent<CircleCollider2D>();

            OnEnterAttractArea = _attractAreaCollider
                .OnTriggerEnter2DAsObservable()
                .Select(o => o.GetComponent<IAttractableEntity>())
                .Where(a => a != null);

            OnExitAttractArea = _attractAreaCollider
                .OnTriggerExit2DAsObservable()
                .Select(o => o.GetComponent<IAttractableEntity>())
                .Where(a => a != null);
        }

        private void OnDestroy()
        {
            _onDestroySubject.OnNext(Id);
            _onDestroySubject.OnCompleted();
        }

        public EntityId Id { get; private set; }

        public bool IsActive => isActiveAndEnabled;
        public bool IsDestroyed => !this;
        Observable<EntityId> IEntity.OnDestroy => _onDestroySubject;

        public void Initialize(EntityId id)
        {
            Id = id;
        }

        public Vector2 Position => new(_transform.position.x, _transform.position.y);

        public float GravityConstant => gravityConstant;
        public float SurfaceGravity => surfaceGravity;

        public float Radius => _planetCollider.radius * Mathf.Max(_transform.lossyScale.x, _transform.lossyScale.y);

        public IReadOnlyCollection<IAttractableEntity> AttractablesInArea => _attractablesInArea;
        public Observable<IAttractableEntity> OnEnterAttractArea { get; private set; }
        public Observable<IAttractableEntity> OnExitAttractArea { get; private set; }

        public void AddAttractableInArea(IAttractableEntity attractable)
        {
            _attractablesInArea.Add(attractable);
        }

        public void RemoveAttractableFromArea(IAttractableEntity attractable)
        {
            _attractablesInArea.Remove(attractable);
        }
    }
}