using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public interface IPlayerJumpLogic
    {
        void OnJumpInput(IPlayerMovementCore playerMovement, InputState inputState);
        void UpdateJumpCharge(IPlayerMovementCore playerMovement);
        void FixedUpdateJump(IPlayerMovementCore playerMovement);
    }
}