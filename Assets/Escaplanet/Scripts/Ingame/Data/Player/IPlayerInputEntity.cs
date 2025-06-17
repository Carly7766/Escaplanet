using Escaplanet.Ingame.Data.Common;
using R3;

namespace Escaplanet.Ingame.Data.Player
{
    public interface IPlayerInputEntity : IEntity
    {
        float MoveInput { get; }
        Observable<Unit> OnJump { get; }
    }
}