using Escaplanet.Root.Core.Common.ValueObject;
using R3;

namespace Escaplanet.Result.GameLogic
{
    public interface IResultInputCore
    {
        Observable<InputState> OnInputTransition { get; }
    }
}