using Escaplanet.Root.Common.ValueObject;
using R3;

namespace Escaplanet.Escaplanet.Title.Core
{
    public interface ITitleInputCore
    {
        Observable<InputState> OnInputTransition { get; }
    }
}