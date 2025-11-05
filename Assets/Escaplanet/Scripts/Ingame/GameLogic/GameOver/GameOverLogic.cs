using Escaplanet.Ingame.Core.GameOver;

namespace Escaplanet.Ingame.GameLogic.GameOver
{
    public class GameOverLogic
    {
        private readonly IGameOverLogicCore _gameOverLogicCore;

        public GameOverLogic(IGameOverLogicCore gameOverLogicCore)
        {
            _gameOverLogicCore = gameOverLogicCore;
        }

        public void GameOver()
        {
            _gameOverLogicCore.GameOver();
        }
    }
}