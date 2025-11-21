using System;
using Escaplanet.Ingame.Core.Gimmick.GoalFlag;
using Escaplanet.Ingame.GameLogic.GameClear;
using Escaplanet.Root.Core;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Gimmick
{
    public class GoalFlagEntryPoint : IStartable, IDisposable
    {
        private IGameInfoCore _gameInfoCore;
        private IGoalFlagCore _goalFlagCore;

        private GameClearLogic _gameClearLogic;

        private readonly CompositeDisposable _disposables = new();

        public GoalFlagEntryPoint(IGameInfoCore gameInfoCore, IGoalFlagCore goalFlagCore, GameClearLogic gameClearLogic)
        {
            _gameInfoCore = gameInfoCore;
            _goalFlagCore = goalFlagCore;
            _gameClearLogic = gameClearLogic;
        }


        public void Start()
        {
            _goalFlagCore.OnGoalReached
                .Subscribe(_ => _gameClearLogic.GameClear(_gameInfoCore))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}