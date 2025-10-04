using System;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common.ValueObject;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Escaplanet.Ingame.Presentation.Player
{
    public class PlayerInputComponent : MonoBehaviour, IPlayerInputCore
    {
        [SerializeField] private PlayerInput playerInput;

        private Subject<ScalarFloat> _onMoveInput = new();
        private Subject<Unit> _onJumpInput = new();

        public ScalarFloat MoveInput { get; private set; }
        public Observable<Unit> OnJumpInput => _onJumpInput;

        private void Awake()
        {
            _onJumpInput = new Subject<Unit>();

            playerInput.actions["Move"].performed += OnMovePerformed;
            playerInput.actions["Move"].canceled += OnMovePerformed;

            playerInput.actions["Jump"].performed += ctx => OnJump();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<float>();
            MoveInput = new ScalarFloat(input);
        }

        public void OnJump()
        {
            _onJumpInput.OnNext(Unit.Default);
        }
    }
}