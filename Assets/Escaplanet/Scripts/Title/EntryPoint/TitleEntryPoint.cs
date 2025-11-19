using Escaplanet.Escaplanet.Title.Core;
using Escaplanet.Root.Core;
using Escaplanet.Root.GameLogic;
using R3;
using VContainer.Unity;

namespace Escaplanet.Escaplanet.Title.EntryPoint
{
    public class TitleEntryPoint : IInitializable, IStartable
    {
        private ITitleInputCore _titleInputCore;

        private SceneTransitionLogic _sceneTransitionLogic;
        private GameStateChangeLogic _gameStateChangeLogic;

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
            _titleInputCore.OnInputTransition.Subscribe(_ => _sceneTransitionLogic.Transition(GameState.Ingame));
        }
    }
}