using System.Collections.Generic;
using Escaplanet.Mockup.Ingame.Attractable;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Escaplanet.Mockup.Ingame.Planet
{
    public class PlanetAttractor : MonoBehaviour
    {
        public float surfaceGravity = 1;
        public float gravityConstant = 6.674e-11f;

        private float AttractAreaRadius =>
            attractArea.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);

        private float PlanetMass => surfaceGravity * (AttractAreaRadius * AttractAreaRadius) / gravityConstant;

        [SerializeField] private CircleCollider2D planetCollider;
        [SerializeField] private CircleCollider2D attractArea;

        private readonly HashSet<IAttractable> _attractables = new HashSet<IAttractable>();

        private void Awake()
        {
            attractArea.OnTriggerEnter2DAsObservable().Subscribe(other =>
            {
                if (other.TryGetComponent<IAttractable>(out var attractable))
                {
                    _attractables.Add(attractable);
                }
            }).AddTo(attractArea);

            attractArea.OnTriggerExit2DAsObservable().Subscribe(other =>
            {
                if (other.TryGetComponent<IAttractable>(out var attractable))
                {
                    _attractables.Remove(attractable);
                }
            }).AddTo(attractArea);
        }

        private void FixedUpdate()
        {
            foreach (var attractable in _attractables)
            {
                var diff = (Vector2)transform.position - attractable.Position;
                var direction = diff.normalized;
                var distance = diff.magnitude;

                var accelerationSpeed = gravityConstant * PlanetMass / (distance * distance);

                var acceleration = direction * accelerationSpeed;
                attractable.Attract(acceleration);
            }
        }
    }
}