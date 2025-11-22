using System;
using Escaplanet.Ingame.Core.Gimmick.BlackHole;
using Escaplanet.Ingame.GameLogic.GameOver;
using Escaplanet.Root.Core;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Gimmick
{
    public class BlackHoleEntryPoint : IStartable, IDisposable
    {
        IGameInfoCore _gameInfoCore;
        IBlackHoleCore _blackHoleCore;

        GameOverLogic _gameOverLogic;
        CompositeDisposable _disposables = new();

        public BlackHoleEntryPoint(IGameInfoCore gameInfoCore, IBlackHoleCore blackHoleCore,
            GameOverLogic gameOverLogic)
        {
            _gameInfoCore = gameInfoCore;
            _blackHoleCore = blackHoleCore;
            _gameOverLogic = gameOverLogic;
        }

        public void Start()
        {
            _blackHoleCore.OnTouchPlayer
                .Subscribe(_ => _gameOverLogic.GameOver(_gameInfoCore))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}