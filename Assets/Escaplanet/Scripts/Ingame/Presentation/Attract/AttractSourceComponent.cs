using System.Collections.Generic;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Root.Common.ValueObject;
using R3;
using R3.Triggers;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Attract
{
    public class AttractSourceComponent : MonoBehaviour, IAttractSourceCore
    {
        [SerializeField] private float gravityConstant = 6.67430e-11f;
        [SerializeField] private float surfaceGravity = 1.0f;

        private Transform _transform;
        private CircleCollider2D _planetCollider;
        private CircleCollider2D _attractAreaCollider;

        private readonly HashSet<IReadonlyAttractableCore> _attractablesInArea = new();


        public Vector2 Position => new(_transform.position.x, _transform.position.y);

        public ScalarFloat GravityConstant => new(gravityConstant);
        public ScalarFloat SurfaceGravity => new(surfaceGravity);

        public ScalarFloat Radius =>
            new(_planetCollider.radius * Mathf.Max(_transform.localScale.x, _transform.localScale.y));

        public IReadOnlyCollection<IReadonlyAttractableCore> AttractablesInArea => _attractablesInArea;

        public Observable<Unit> OnAttractUpdate { get; private set; }
        public Observable<IAttractableCore> OnEnterAttractArea { get; private set; }
        public Observable<IAttractableCore> OnExitAttractArea { get; private set; }


        private void Awake()
        {
            _transform = transform;
            _planetCollider = GetComponent<CircleCollider2D>();
            _attractAreaCollider = _transform.GetChild(0).GetComponent<CircleCollider2D>();

            OnAttractUpdate = _attractAreaCollider
                .FixedUpdateAsObservable()
                .AsUnitObservable()
                .TakeUntil(destroyCancellationToken);

            OnEnterAttractArea = _attractAreaCollider
                .OnTriggerEnter2DAsObservable()
                .Select(o => o.GetComponent<IAttractableCore>())
                .Where(a => a != null)
                .TakeUntil(destroyCancellationToken);

            OnExitAttractArea = _attractAreaCollider
                .OnTriggerExit2DAsObservable()
                .Select(o => o.GetComponent<IAttractableCore>())
                .Where(a => a != null)
                .TakeUntil(destroyCancellationToken);
        }

        public void AddAttractableInArea(IReadonlyAttractableCore attractable)
        {
            _attractablesInArea.Add(attractable);
        }

        public void RemoveAttractableInArea(IReadonlyAttractableCore attractable)
        {
            _attractablesInArea.Remove(attractable);
        }
    }
}