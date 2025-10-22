using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using R3;
using R3.Triggers;
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
        [SerializeField] private bool isJumping;
        
        [SerializeField] private float jumpPower;
        [SerializeField] private float maxJumpPower = 1f;
        [SerializeField] private float jumpChargeSpeed = 0.1f;
        [SerializeField] private float jumpPowerMultiplier = 5f;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;

        public float MoveSpeed => moveSpeed;
        public float Acceleration => acceleration;
        public float MovementLerpAmount => movementLerp;

        public bool IsFlayingAway
        {
            get => isFlayingAway;
            set => isFlayingAway = value;
        }

        public bool IsJumping
        {
            get => isJumping;
            set => isJumping = value;
        }

        public float MaxJumpPower => maxJumpPower;
        public float JumpChargeSpeed => jumpChargeSpeed;
        public float JumpPowerMultiplier => jumpPowerMultiplier;
        public float JumpPower
        {
            get => jumpPower;
            set => jumpPower = value;
        }

        public bool IsChargingJump { get; set; }

        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public Vector2 Up => new(_transform.up.x, _transform.up.y);
        public Vector2 Velocity => new(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);
        public Observable<Unit> OnGrounded { get; private set; }

        private void Awake()
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();

            OnGrounded = _collider2D.OnCollisionEnter2DAsObservable()
                .Where(o => o.gameObject.TryGetComponent<IAttractSourceCore>(out _))
                .Select(_ => Unit.Default)
                .TakeUntil(destroyCancellationToken);
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