using Escaplanet.Result.Core;
using Escaplanet.Result.GameLogic;
using Escaplanet.Root.Core;
using Escaplanet.Root.GameLogic;
using VContainer.Unity;

namespace Escaplanet.Result.EntryPoint
{
    public class ResultEntryPoint : IInitializable
    {
        private readonly IGameInfoCore _gameInfoCore;

        private readonly GameStateChangeLogic _gameStateChangeLogic;
        private readonly IResultDisplayCore _resultDisplayCore;
        private readonly ResultDisplayLogic _resultDisplayLogic;

        public ResultEntryPoint(IGameInfoCore gameInfoCore, IResultDisplayCore resultDisplayCore,
            GameStateChangeLogic gameStateChangeLogic, ResultDisplayLogic resultDisplayLogic)
        {
            _gameInfoCore = gameInfoCore;
            _resultDisplayCore = resultDisplayCore;
            _gameStateChangeLogic = gameStateChangeLogic;
            _resultDisplayLogic = resultDisplayLogic;
        }

        public void Initialize()
        {
            _gameStateChangeLogic.ChangeGameState(GameState.Result);
            _resultDisplayLogic.Display(_gameInfoCore.CurrentGameResult, _resultDisplayCore);
        }
    }
}