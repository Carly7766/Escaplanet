using Escaplanet.Ingame.Core.Player;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public interface IPlayerJumpChargeLogic
    {
        void StartJumpCharge();
        void Jump();
        void UpdateJumpCharge();
    }
}