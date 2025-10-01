using System;
using System.Collections.Generic;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.GameLogic.Attract;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Attract
{
    public class AttractableEntryPoint : IStartable, IFixedTickable, IDisposable
    {
        private readonly IAttractableCore _attractable;
        private readonly IAttractAreaDetectionLogic _attractAreaDetectionLogic;
        private readonly IAttractUpdateLogic _attractUpdateLogic;
        private readonly IRotateUpdateLogic _rotateUpdateLogic;

        private readonly CompositeDisposable _disposable = new();

        public AttractableEntryPoint(IAttractableCore attractable,
            IAttractAreaDetectionLogic attractAreaDetectionLogic,
            IAttractUpdateLogic attractUpdateLogic)
        {
            _attractable = attractable;
            _attractAreaDetectionLogic = attractAreaDetectionLogic;
            _attractUpdateLogic = attractUpdateLogic;
        }


        public void Start()
        {
            _attractable.OnEnterAttractArea
                .Subscribe(source => { _attractAreaDetectionLogic.OnSourceEnter(source, _attractable); })
                .AddTo(_disposable);

            _attractable.OnExitAttractArea
                .Subscribe(source => { _attractAreaDetectionLogic.OnSourceExit(source, _attractable); })
                .AddTo(_disposable);
        }

        public void FixedTick()
        {
            _attractAreaDetectionLogic.ExcludeDestroyedSources(_attractable);
            _attractUpdateLogic.UpdateAttract(_attractable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}