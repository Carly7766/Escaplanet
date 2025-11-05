using System.Threading;
using Escaplanet.Ingame.Core.GameOver;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.GameOver
{
    public class GameOverDetectableComponent : MonoBehaviour, IGameOverDetectableCore
    {
        [SerializeField] private GameOverState currentState;
        public GameOverState CurrentState => currentState;

        public void SetState(GameOverState newState)
        {
            currentState = newState;
        }

        public float GraceTimer { get; set; }
        public CancellationTokenSource CountdownCancellationTokenSource { get; set; }
    }
}