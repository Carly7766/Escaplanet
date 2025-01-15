using UnityEngine;

namespace Escaplanet.Mockup.Ingame.Attractable
{
    public class AttractableView : MonoBehaviour, IAttractable
    {
        public Vector2 Position => transform.position;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Attract(Vector2 acceleration)
        {
            _rigidbody2D.AddForce(acceleration, ForceMode2D.Force);
        }
    }
}