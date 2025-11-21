using Escaplanet.Root.Core;
using Escaplanet.Root.GameLogic;

namespace Escaplanet.Ingame.GameLogic.GameClear
{
    public class GameClearLogic
    {
        SceneTransitionLogic _sceneTransitionLogic;

        public GameClearLogic(SceneTransitionLogic sceneTransitionLogic)
        {
            _sceneTransitionLogic = sceneTransitionLogic;
        }

        public void GameClear(IGameInfoCore gameInfoCore)
        {
            gameInfoCore.CurrentGameResult = GameResult.Clear;

            _sceneTransitionLogic.Transition(GameState.Result);
        }
    }
}