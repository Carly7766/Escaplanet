using Escaplanet.Mockup.Ingame.Attractable;
using UnityEngine;

namespace Escaplanet.Mockup.Ingame.Player
{
    public class PlayerAttractable : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private Transform planetTransform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var direction = (planetTransform.position - _transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody2D.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward).eulerAngles.z;
        }
    }
}