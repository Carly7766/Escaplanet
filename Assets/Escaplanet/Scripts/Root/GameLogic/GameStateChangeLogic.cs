using Escaplanet.Root.Core;

namespace Escaplanet.Root.GameLogic
{
    public class GameStateChangeLogic
    {
        private readonly IGameInfoCore _gameInfoCore;

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