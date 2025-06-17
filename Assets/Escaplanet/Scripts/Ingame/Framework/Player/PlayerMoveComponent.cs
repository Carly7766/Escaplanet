using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.Data.EntityId;
using Escaplanet.Ingame.Data.Player;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using NotImplementedException = System.NotImplementedException;
using Vector2 = Escaplanet.Ingame.Data.Common.Vector2;

namespace Escaplanet.Ingame.Framework.Player
{
    public class PlayerMoveComponent : MonoBehaviour, IPlayerMoveEntity
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float movementLerpAmount = 1f;
        [SerializeField] private float rotateSpeed = 10f; // Degrees per second

        private readonly Subject<EntityId> _onDestroySubject = new();

        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            _onDestroySubject.OnNext(Id);
            _onDestroySubject.OnCompleted();
        }

        public void Initialize(EntityId id)
        {
            Id = id;
        }

        public EntityId Id { get; private set; }
        public bool IsActive => isActiveAndEnabled;
        public bool IsDestroyed => !this;
        Observable<EntityId> IEntity.OnDestroy => _onDestroySubject;


        public float MoveSpeed => moveSpeed;
        public float Acceleration => acceleration;
        public float MovementLerpAmount => movementLerpAmount;
        public bool IsFlayingAway { get; set; }
        public float RotateSpeed => rotateSpeed;
        public Vector2 Position => new(_transform.position.x, _transform.position.y);
        public float Rotation => _transform.rotation.eulerAngles.z;
        public Vector2 Velocity => new(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);

        public void Move(Vector2 velocity)
        {
            _rigidbody2D.AddForce(new UnityEngine.Vector2(velocity.X, velocity.Y));
        }

        public void Rotate(float angle)
        {
            _rigidbody2D.MoveRotation(angle);
        }

        //TODO: System側に実装を移す
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Planet")) IsFlayingAway = false;
        }
    }
}