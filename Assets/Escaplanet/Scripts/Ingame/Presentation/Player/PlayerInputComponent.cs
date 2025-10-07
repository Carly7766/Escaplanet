using System;
using Escaplanet.Ingame.Core.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Escaplanet.Ingame.Presentation.Player
{
    public class PlayerInputComponent : MonoBehaviour, IPlayerInputCore
    {
        private PlayerInput _playerInput;

        public float MoveInput { get; private set; }

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
        }
    }
}