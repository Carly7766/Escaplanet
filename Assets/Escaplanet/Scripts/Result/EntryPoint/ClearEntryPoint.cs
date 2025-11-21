using System;
using Escaplanet.Result.GameLogic;
using Escaplanet.Root.Core;
using Escaplanet.Root.GameLogic;
using R3;
using VContainer.Unity;

namespace Escaplanet.Result.EntryPoint
{
    public class ClearEntryPoint : IStartable, IDisposable
    {
        private IResultInputCore _playerInputCore;

        private SceneTransitionLogic _sceneTransitionLogic;

        private CompositeDisposable _disposables = new();

        public ClearEntryPoint(IResultInputCore playerInputCore, SceneTransitionLogic sceneTransitionLogic)
        {
            _playerInputCore = playerInputCore;
            _sceneTransitionLogic = sceneTransitionLogic;
        }

        public void Start()
        {
            _playerInputCore.OnInputTransition
                .Subscribe(_ => _sceneTransitionLogic.Transition(GameState.Title))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}