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

        private readonly HashSet<IReadonlyAttractableCore> _attractablesInArea = new();


        public Vector2 Position => new(_transform.position.x, _transform.position.y);

        public ScalarFloat GravityConstant => new(gravityConstant);
        public ScalarFloat SurfaceGravity => new(surfaceGravity);

        public ScalarFloat Radius =>
            new(_planetCollider.radius * Mathf.Max(_transform.localScale.x, _transform.localScale.y));

        public bool IsDestroyed => this == null;

        public IReadOnlyCollection<IReadonlyAttractableCore> AttractablesInArea => _attractablesInArea;
        
        
        private void Awake()
        {
            _transform = transform;
            _planetCollider = GetComponent<CircleCollider2D>();
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