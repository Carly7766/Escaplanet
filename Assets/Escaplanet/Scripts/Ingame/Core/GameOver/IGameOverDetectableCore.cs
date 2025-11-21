using System.Threading;

namespace Escaplanet.Ingame.Core.GameOver
{
    public interface IGameOverDetectableCore
    {
        GameOverState CurrentState { get; }

        float GraceTimer { get; set; }

        CancellationTokenSource CountdownCancellationTokenSource { get; set; }
        void SetState(GameOverState newState);
    }
}