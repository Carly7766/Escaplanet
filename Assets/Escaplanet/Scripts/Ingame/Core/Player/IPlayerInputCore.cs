using R3;

namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerInputCore
    {
        float MoveInput { get; }

        Observable<Unit> OnJumpInputDown { get; }
        Observable<Unit> OnJumpInputUp { get; }
    }
}