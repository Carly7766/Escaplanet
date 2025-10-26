using Escaplanet.Ingame.Core.Player;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerGroundDetectionLogic
    {
        IPlayerMovementCore _playerMovementCore;

        public PlayerGroundDetectionLogic(IPlayerMovementCore playerMovementCore)
        {
            _playerMovementCore = playerMovementCore;
        }

        public void OnGroundDetected()
        {
            _playerMovementCore.IsFlayingAway = false;
            _playerMovementCore.IsJumping = false;
        }
    }
}