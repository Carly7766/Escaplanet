using Escaplanet.Result.GameLogic;
using Escaplanet.Root.Core.Common.ValueObject;
using R3;
using UnityEngine;

namespace Escaplanet.Result.Presentation
{
    public class ResultInputComponent : MonoBehaviour, IResultInputCore
    {
        private readonly Subject<InputState> _onInputTransitionSubject = new();
        public Observable<InputState> OnInputTransition => _onInputTransitionSubject;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _onInputTransitionSubject.OnNext(InputState.Down);
        }
    }
}