using System;
using Escaplanet.Ingame.Core.Player;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.Player
{
    public class PlayerAppearanceComponent : MonoBehaviour, IPlayerAppearanceCore
    {
        private Transform _playerSpriteTransform;

        public bool IsFacingRight { get; private set; }

        private void Awake()
        {
            _playerSpriteTransform = transform.GetChild(0);
        }

        public void Flip(bool facingRight)
        {
            if (facingRight)
            {
                _playerSpriteTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _playerSpriteTransform.localScale = new Vector3(-1, 1, 1);
            }

            IsFacingRight = facingRight;
        }
    }
}