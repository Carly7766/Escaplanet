using Escaplanet.Ingame.Core.AttractSystem;
using UnityEngine;

namespace Escaplanet.Ingame.Framework.AttractSystem
{
    public class AttractableEntity : MonoBehaviour, IAttractableEntity
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        public bool IsNullEntity => this == null;
        public Core.Common.Vector2 Position => new(_transform.position.x, _transform.position.y);
        public float Mass => _rigidbody2D.mass;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Attract(Core.Common.Vector2 force)
        {
            _rigidbody2D.AddForce(new Vector2(force.X, force.Y));
        }
    }
}