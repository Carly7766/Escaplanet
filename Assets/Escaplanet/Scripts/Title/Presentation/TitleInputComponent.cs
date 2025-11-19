using Escaplanet.Escaplanet.Title.Core;
using Escaplanet.Root.Common.ValueObject;
using R3;
using UnityEngine;

namespace Escaplanet.Escaplanet.Title.Presentation
{
    public class TitleInputComponent : MonoBehaviour, ITitleInputCore
    {
        private Subject<InputState> _onInputTransitionSubject = new();
        public Observable<InputState> OnInputTransition => _onInputTransitionSubject;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _onInputTransitionSubject.OnNext(InputState.Down);
            }
        }
    }
}