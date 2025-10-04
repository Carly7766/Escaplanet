using Escaplanet.Root.Common.ValueObject;
using R3;

namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerInputCore
    {
        ScalarFloat MoveInput { get; }
        Observable<Unit> OnJumpInput { get; }
    }
}