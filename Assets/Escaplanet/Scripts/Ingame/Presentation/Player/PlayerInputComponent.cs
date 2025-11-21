using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Core.Common.ValueObject;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Escaplanet.Ingame.Presentation.Player
{
    public class PlayerInputComponent : MonoBehaviour, IPlayerInputCore
    {
        private readonly Subject<InputState> _jumpInputSubject = new();

        private readonly Subject<InputState> _switchCameraInputSubject = new();
        private PlayerInput _playerInput;

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

        public float MoveInput { get; private set; }
        public Observable<InputState> OnJumpInput => _jumpInputSubject;
        public Observable<InputState> OnSwitchCameraInput => _switchCameraInputSubject;

        private void OnInputAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Movement")
                MoveInput = context.ReadValue<float>();

            if (context.action.name == "Jump")
            {
                if (context.started)
                    _jumpInputSubject.OnNext(InputState.Down);
                else if (context.performed)
                    _jumpInputSubject.OnNext(InputState.Hold);
                else if (context.canceled) _jumpInputSubject.OnNext(InputState.Up);
            }

            if (context.action.name == "SwitchCamera")
            {
                if (context.started)
                    _switchCameraInputSubject.OnNext(InputState.Down);
                else if (context.performed)
                    _switchCameraInputSubject.OnNext(InputState.Hold);
                else if (context.canceled) _switchCameraInputSubject.OnNext(InputState.Up);
            }
        }
    }
}