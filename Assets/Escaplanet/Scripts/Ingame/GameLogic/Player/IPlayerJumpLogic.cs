using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public interface IPlayerJumpLogic
    {
        void OnJumpInput(InputState inputState);
        void UpdateJump();
    }
}