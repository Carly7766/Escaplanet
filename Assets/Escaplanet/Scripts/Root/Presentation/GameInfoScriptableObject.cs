using Escaplanet.Root.Core;
using UnityEngine;

namespace Escaplanet.Root.Presentation
{
    [CreateAssetMenu(fileName = "GameInfoScriptableObject",
        menuName = "Escaplanet/Root/Presentation/GameInfoScriptableObject")]
    public class GameInfoScriptableObject : ScriptableObject, IGameInfoCore
    {
        [SerializeField] private GameState gameState;
        [SerializeField] private GameResult gameResult;
        [SerializeField] private GameOverType gameOverType;

        public GameState CurrentGameState
        {
            get => gameState;
            set => gameState = value;
        }

        public GameResult CurrentGameResult
        {
            get => gameResult;
            set => gameResult = value;
        }

        public GameOverType CurrentGameOverType
        {
            get => gameOverType;
            set => gameOverType = value;
        }
    }
}