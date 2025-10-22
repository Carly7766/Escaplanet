using System;
using Escaplanet.Ingame.Core.Player;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Escaplanet.Ingame.Presentation.Player
{
    public class PlayerInputComponent : MonoBehaviour, IPlayerInputCore
    {
        private PlayerInput _playerInput;

        public float MoveInput { get; private set; }

        private readonly Subject<Unit> _onJumpInputDown = new();
        private readonly Subject<Unit> _onJumpInputUp = new();

        public Observable<Unit> OnJumpInputDown => _onJumpInputDown;
        public Observable<Unit> OnJumpInputUp => _onJumpInputUp;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            if (_playerInput == null) return;
            _playerInput.onActionTriggered += OnInputAction;
        }

        private void OnDisable()
        {
            if (_playerInput == null) return;
            _playerInput.onActionTriggered -= OnInputAction;
        }

        private void OnInputAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Move")
                MoveInput = context.ReadValue<float>();
            if (context.action.name == "Jump")
            {
                if (context.started)
                {
                    _onJumpInputDown.OnNext(Unit.Default);
                }
                else if (context.canceled)
                {
                    _onJumpInputUp.OnNext(Unit.Default);
                }
            }
        }
    }
}