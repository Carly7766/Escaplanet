using System;
using Escaplanet.Result.Core;
using Escaplanet.Root.Core;

namespace Escaplanet.Result.GameLogic
{
    public class ResultDisplayLogic
    {
        public void Display(GameResult gameResult, IResultDisplayCore resultDisplay)
        {
            switch (gameResult)
            {
                case GameResult.None:
                    resultDisplay.DisplayNone();
                    break;
                case GameResult.GameOver:
                    resultDisplay.DisplayGameOver();
                    break;
                case GameResult.Clear:
                    resultDisplay.DisplayClear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameResult), gameResult, null);
            }
        }
    }
}