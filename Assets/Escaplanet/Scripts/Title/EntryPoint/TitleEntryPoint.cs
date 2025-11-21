using System;
using Escaplanet.Root.Core;
using Escaplanet.Root.GameLogic;
using Escaplanet.Title.Core;
using R3;
using VContainer.Unity;

namespace Escaplanet.Title.EntryPoint
{
    public class TitleEntryPoint : IInitializable, IStartable, IDisposable
    {
        private readonly GameStateChangeLogic _gameStateChangeLogic;

        private readonly SceneTransitionLogic _sceneTransitionLogic;
        private readonly ITitleInputCore _titleInputCore;

        private readonly CompositeDisposable _disposables = new();

        public TitleEntryPoint(ITitleInputCore titleInputCore, SceneTransitionLogic sceneTransitionLogic,
            GameStateChangeLogic gameStateChangeLogic)
        {
            _titleInputCore = titleInputCore;
            _sceneTransitionLogic = sceneTransitionLogic;
            _gameStateChangeLogic = gameStateChangeLogic;
        }

        public void Initialize()
        {
            _gameStateChangeLogic.ChangeGameState(GameState.Title);
        }

        public void Start()
        {
            _titleInputCore.OnInputTransition
                .Subscribe(_ => _sceneTransitionLogic.Transition(GameState.Ingame))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}