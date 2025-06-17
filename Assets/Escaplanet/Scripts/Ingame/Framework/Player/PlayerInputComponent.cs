using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.Data.EntityId;
using Escaplanet.Ingame.Data.Player;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using NotImplementedException = System.NotImplementedException;

namespace Escaplanet.Ingame.Framework.Player
{
    public class PlayerInputComponent : MonoBehaviour, IPlayerInputEntity
    {
        [SerializeField] private InputActionReference actionMove;
        [SerializeField] private InputActionReference actionJump;
        public float MoveInput { get; private set; }
        private Subject<Unit> onJumpSubject = new();
        public Observable<Unit> OnJump => onJumpSubject;

        private IEntityIdGenerator _entityIdGenerator;
        private Subject<EntityId> onDestroySubject = new();

        private void Awake()
        {
            actionMove.action.performed += ctx => MoveInput = ctx.ReadValue<float>();
            actionMove.action.canceled += ctx => MoveInput = 0f;

            actionJump.action.performed += ctx => onJumpSubject.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            onDestroySubject.OnNext(Id);
            onDestroySubject.OnCompleted();
            Dispose();
        }

        private void OnEnable()
        {
            actionMove.action.Enable();
            actionJump.action.Enable();
        }

        private void OnDisable()
        {
            actionMove.action.Disable();
            actionJump.action.Disable();
        }

        public EntityId Id { get; private set; }
        public bool IsActive => isActiveAndEnabled;
        public bool IsDestroyed => !this;
        Observable<EntityId> IEntity.OnDestroy => onDestroySubject;

        public void Initialize(IEntityIdGenerator entityIdGenerator)
        {
            _entityIdGenerator = entityIdGenerator;
            Id = entityIdGenerator.Generate();
        }

        public void Dispose()
        {
            _entityIdGenerator.Recycle(Id);
        }
    }
}