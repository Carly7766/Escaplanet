﻿using Escaplanet.Ingame.Core.Player;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerGroundDetectionLogic
    {
        public void OnGroundDetected(IPlayerMovementCore playerMovementCore)
        {
            playerMovementCore.IsFlayingAway = false;
            playerMovementCore.IsJumping = false;
        }
    }
}