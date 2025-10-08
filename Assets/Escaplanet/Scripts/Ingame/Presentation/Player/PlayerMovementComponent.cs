using Escaplanet.Ingame.Core.Player;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Player
{
    public class PlayerMovementComponent : MonoBehaviour, IPlayerMovementCore
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField, Range(0f, 1f)] private float movementLerp = 1f;

        [SerializeField] private bool isFlayingAway;

        [SerializeField] private float maxJumpPower = 1f;
        [SerializeField] private float jumpChargeSpeed = 0.1f;
        [SerializeField] private float jumpPowerMultiplier = 5f;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        public float MoveSpeed => moveSpeed;
        public float Acceleration => acceleration;
        public float MovementLerpAmount => movementLerp;

        public bool IsFlayingAway
        {
            get => isFlayingAway;
            set => isFlayingAway = value;
        }

        public float MaxJumpPower => maxJumpPower;
        public float JumpChargeSpeed => jumpChargeSpeed;
        public float JumpPowerMultiplier => jumpPowerMultiplier;
        public float JumpPower { get; set; }
        public bool IsChargingJump { get; set; }

        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public Vector2 Up => new(_transform.up.x, _transform.up.y);
        public Vector2 Velocity => new(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);

        private void Awake()
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 velocity)
        {
            _rigidbody2D.AddForce(new UnityEngine.Vector2(velocity.X, velocity.Y));
        }

        public void Jump(Vector2 force)
        {
            _rigidbody2D.AddForce(new UnityEngine.Vector2(force.X, force.Y), ForceMode2D.Impulse);
        }
    }
}