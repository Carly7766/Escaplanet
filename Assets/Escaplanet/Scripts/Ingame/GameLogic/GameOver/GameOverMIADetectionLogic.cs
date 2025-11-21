using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.GameOver;
using Escaplanet.Ingame.Core.UI;
using Escaplanet.Root.Core;
using Escaplanet.Root.Core.Common;

namespace Escaplanet.Ingame.GameLogic.GameOver
{
    public class GameOverMiaDetectionLogic : IDisposable
    {
        private readonly GameOverLogic _gameOverLogic;
        private readonly IGameOverPolicy _gameOverPolicy;

        private readonly IGlobalValuePort _globalValuePort;

        private readonly CancellationTokenSource _logicCts = new();

        public GameOverMiaDetectionLogic(IGameOverPolicy gameOverPolicy, GameOverLogic gameOverLogic,
            IGlobalValuePort globalValuePort)
        {
            _gameOverPolicy = gameOverPolicy;
            _gameOverLogic = gameOverLogic;
            _globalValuePort = globalValuePort;
        }

        public void Dispose()
        {
            _logicCts?.Cancel();
            _logicCts?.Dispose();
        }

        public void Update(IAttractableCore attractable, IGameOverDetectableCore gameOverDetectable,
            ICountdownTextCore countdownText, IGameInfoCore gameInfoCore, CancellationToken token = default)
        {
            switch (gameOverDetectable.CurrentState)
            {
                case GameOverState.Grounded:
                    if (attractable.NearestSource == null)
                    {
                        gameOverDetectable.GraceTimer = 0f;
                        gameOverDetectable.SetState(GameOverState.GracePeriod);
                    }

                    break;
                case GameOverState.GracePeriod:
                    if (attractable.NearestSource != null)
                    {
                        gameOverDetectable.SetState(GameOverState.Grounded);
                        break;
                    }

                    gameOverDetectable.GraceTimer += _globalValuePort.DeltaTime;
                    if (gameOverDetectable.GraceTimer >= _gameOverPolicy.GraceSeconds)
                    {
                        gameOverDetectable.CountdownCancellationTokenSource = new CancellationTokenSource();
                        var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _logicCts.Token,
                            gameOverDetectable.CountdownCancellationTokenSource.Token);

                        countdownText.Show();
                        BeginCountdownFlowAsync(countdownText, gameInfoCore, linkedCts.Token).Forget();
                        gameOverDetectable.SetState(GameOverState.Countdown);
                    }

                    break;
                case GameOverState.Countdown:
                    if (attractable.NearestSource != null)
                    {
                        countdownText.Hide();
                        gameOverDetectable.CountdownCancellationTokenSource?.Cancel();
                        gameOverDetectable.SetState(GameOverState.Grounded);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async UniTaskVoid BeginCountdownFlowAsync(ICountdownTextCore countdownText, IGameInfoCore gameInfo,
            CancellationToken token = default)
        {
            await countdownText.RunCountdownAsync(_gameOverPolicy.CountdownSeconds, token);
            _gameOverLogic.GameOver(gameInfo);
        }
    }
}