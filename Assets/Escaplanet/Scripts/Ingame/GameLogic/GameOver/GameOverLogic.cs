using Escaplanet.Root.Core;
using Escaplanet.Root.GameLogic;

namespace Escaplanet.Ingame.GameLogic.GameOver
{
    public class GameOverLogic
    {
        SceneTransitionLogic _sceneTransitionLogic;

        public GameOverLogic(SceneTransitionLogic sceneTransitionLogic)
        {
            _sceneTransitionLogic = sceneTransitionLogic;
        }

        public void GameOver(IGameInfoCore gameInfoCore)
        {
            gameInfoCore.CurrentGameResult = GameResult.GameOver;
            gameInfoCore.CurrentGameOverType = GameOverType.SpaceDrifting;

            _sceneTransitionLogic.Transition(GameState.Result);
        }
    }
}