using Escaplanet.Root.Core;
using Escaplanet.Root.Core.Common;

namespace Escaplanet.Root.GameLogic
{
    public class SceneTransitionLogic
    {
        private readonly ISceneLoadPort _sceneLoadPort;

        public SceneTransitionLogic(ISceneLoadPort sceneLoadPort)
        {
            _sceneLoadPort = sceneLoadPort;
        }

        public void Transition(GameState gameState)
        {
            _sceneLoadPort.LoadScene(gameState);
        }
    }
}