using R3;

namespace Escaplanet.Ingame.Core.Gimmick.BlackHole
{
    public interface IBlackHoleCore
    {
        Observable<Unit> OnTouchPlayer { get; }
    }
}