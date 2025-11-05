using System.Threading;

namespace Escaplanet.Ingame.Core.GameOver
{
    public interface IGameOverDetectableCore
    {
        GameOverState CurrentState { get; }
        void SetState(GameOverState newState);

        float GraceTimer { get; set; }

        CancellationTokenSource CountdownCancellationTokenSource { get; set; }
    }
}