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

        public EntityId Id { get; private set; }
        public bool IsActive => isActiveAndEnabled;
        public bool IsDestroyed => !this;
        private Subject<EntityId> onDestroySubject = new();
        Observable<EntityId> IEntity.OnDestroy => onDestroySubject;

        public void Initialize(EntityId id)
        {
            Id = id;
        }

        public float MoveInput { get; private set; }
        private Subject<Unit> onJumpSubject = new();
        public Observable<Unit> OnJump => onJumpSubject;

        private void Awake()
        {
            actionMove.action.performed += ctx => MoveInput = ctx.ReadValue<float>();
            actionMove.action.canceled += ctx => MoveInput = 0f;

            actionJump.action.performed += ctx => onJumpSubject.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            onJumpSubject.OnCompleted();
            onDestroySubject.OnNext(Id);
            onDestroySubject.OnCompleted();
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
    }
}