using Escaplanet.Root.Common.ValueObject;
using R3;

namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerInputCore
    {
        float MoveInput { get; }
        Observable<InputState> OnJumpInput { get; }
    }
}