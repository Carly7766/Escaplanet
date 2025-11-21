using Escaplanet.Escaplanet.Title.Core;
using Escaplanet.Root.Core.Common.ValueObject;
using R3;
using UnityEngine;

namespace Escaplanet.Escaplanet.Title.Presentation
{
    public class TitleInputComponent : MonoBehaviour, ITitleInputCore
    {
        private readonly Subject<InputState> _onInputTransitionSubject = new();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _onInputTransitionSubject.OnNext(InputState.Down);
        }

        public Observable<InputState> OnInputTransition => _onInputTransitionSubject;
    }
}