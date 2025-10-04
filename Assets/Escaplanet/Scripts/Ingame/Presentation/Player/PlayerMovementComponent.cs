using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common.ValueObject;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Player
{
    public class PlayerMovementComponent : MonoBehaviour, IPlayerMovementCore
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField, Range(0f, 1f)] private float movementLerp;

        [SerializeField] private bool isFlayingAway;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        public ScalarFloat MoveSpeed => new(moveSpeed);
        public ScalarFloat Acceleration => new(acceleration);
        public ScalarFloat MovementLerpAmount => new(movementLerp);

        public bool IsFlayingAway
        {
            get => isFlayingAway;
            set => isFlayingAway = value;
        }

        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public Vector2 Velocity => new(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);

        private void Awake()
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 velocity)
        {
            _rigidbody2D.AddForce(new UnityEngine.Vector2(velocity.X.Value, velocity.Y.Value));
        }
    }
}