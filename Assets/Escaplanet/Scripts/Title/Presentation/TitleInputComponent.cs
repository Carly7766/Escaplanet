using Escaplanet.Root.Core.Common.ValueObject;
using Escaplanet.Title.Core;
using R3;
using UnityEngine;

namespace Escaplanet.Title.Presentation
{
    public class TitleInputComponent : MonoBehaviour, ITitleInputCore
    {
        private readonly Subject<InputState> _onInputTransitionSubject = new();
        public Observable<InputState> OnInputTransition => _onInputTransitionSubject;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _onInputTransitionSubject.OnNext(InputState.Down);
        }
    }
}