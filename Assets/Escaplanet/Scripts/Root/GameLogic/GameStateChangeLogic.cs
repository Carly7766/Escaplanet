using Escaplanet.Root.Core;

namespace Escaplanet.Root.GameLogic
{
    public class GameStateChangeLogic
    {
        private IGameInfoCore _gameInfoCore;

        public GameStateChangeLogic(IGameInfoCore gameInfoCore)
        {
            _gameInfoCore = gameInfoCore;
        }

        public void ChangeGameState(GameState gameState)
        {
            _gameInfoCore.CurrentGameState = gameState;
        }
    }
}