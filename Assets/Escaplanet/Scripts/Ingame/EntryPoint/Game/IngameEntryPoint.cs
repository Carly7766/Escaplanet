using Escaplanet.Root.Core;
using Escaplanet.Root.GameLogic;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Game
{
    public class IngameEntryPoint : IInitializable
    {
        private readonly GameStateChangeLogic _gameStateChangeLogic;

        public IngameEntryPoint(GameStateChangeLogic gameStateChangeLogic)
        {
            _gameStateChangeLogic = gameStateChangeLogic;
        }

        public void Initialize()
        {
            _gameStateChangeLogic.ChangeGameState(GameState.Ingame);
        }
    }
}