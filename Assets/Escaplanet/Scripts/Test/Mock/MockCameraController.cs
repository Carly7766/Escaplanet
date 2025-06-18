using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Escaplanet.Test.Mock
{
    public class MockCameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform[] planetTransforms;

        private Transform NearestPlanetTransform
        {
            get
            {
                return planetTransforms
                    .OrderBy(t => Vector2.Distance(t.position, playerTransform.position))
                    .First();
            }
        }

        private Transform _transform;
        [SerializeField] private float distanceMultiplier = 1.0f;
        [SerializeField] private float lerpAmount = 1.0f;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            var diff = playerTransform.position - NearestPlanetTransform.position;
            var normalizedDiff = diff.normalized;
            var distance = diff.magnitude;

            var angleRad = Mathf.Atan2(normalizedDiff.y, normalizedDiff.x);

            var targetPosition = NearestPlanetTransform.position +
                                 new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) *
                                 (distance * distanceMultiplier);
            
            var lerpedPosition = Vector3.Lerp(_transform.position, targetPosition, Time.deltaTime * lerpAmount);
            lerpedPosition.z = _transform.position.z; // Keep the z position unchanged

            _transform.position = lerpedPosition;
        }
    }
}