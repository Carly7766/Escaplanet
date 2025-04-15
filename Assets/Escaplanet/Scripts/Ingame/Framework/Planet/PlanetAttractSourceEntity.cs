using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core;
using Escaplanet.Ingame.Core.AttractSystem;
using Escaplanet.Ingame.Core.Planet;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Escaplanet.Ingame.Framework.Planet
{
    public class PlanetAttractSourceEntity : MonoBehaviour, IPlanetAttractSourceEntity
    {
        [SerializeField] private float gravityConstant = 6.67430e-11f;
        [SerializeField] private float surfaceGravity = 1.0f;

        private Transform _transform;
        private CircleCollider2D _planetCollider;
        private CircleCollider2D _triggerArea;

        public bool IsNullEntity => this == null;
        public Escaplanet.Ingame.Core.Common.Vector2 Position => new(_transform.position.x, _transform.position.y);
        public float Mass => CalculateMass();
        public float GravityConstant => gravityConstant;

        public Observable<IAttractableEntity> OnEnterAttractArea => _triggerArea.OnTriggerEnter2DAsObservable()
            .Select(other => other.GetComponent<IAttractableEntity>());

        public Observable<IAttractableEntity> OnExitAttractArea => _triggerArea.OnTriggerExit2DAsObservable()
            .Select(other => other.GetComponent<IAttractableEntity>());

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _planetCollider = GetComponent<CircleCollider2D>();
            _triggerArea = _transform.GetChild(0).GetComponentInChildren<CircleCollider2D>();
        }

        private float CalculateMass()
        {
            return surfaceGravity *
                   Mathf.Pow(_planetCollider.radius * Mathf.Max(_transform.lossyScale.x, _transform.lossyScale.y), 2) /
                   gravityConstant;
        }
    }
}