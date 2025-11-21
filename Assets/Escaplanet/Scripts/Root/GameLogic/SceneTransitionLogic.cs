using System;
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
            switch (gameState)
            {
                case GameState.Title:
                    _sceneLoadPort.LoadTitleScene();
                    break;
                case GameState.Ingame:
                    _sceneLoadPort.LoadIngameScene();
                    break;
                case GameState.Result:
                    _sceneLoadPort.LoadResultScene();
                    break;
                case GameState.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    }
}