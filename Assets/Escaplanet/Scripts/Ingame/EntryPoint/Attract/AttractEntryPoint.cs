using System;
using System.Collections.Generic;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.GameLogic.Attract;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Attract
{
    public class AttractEntryPoint : IStartable, IDisposable
    {
        private IEnumerable<IAttractSourceCore> _attractables;
        private readonly IAttractAreaDetectionLogic _attractAreaDetectionLogic;
        private readonly IAttractUpdateLogic _attractUpdateLogic;
        private readonly IRotateUpdateLogic _rotateUpdateLogic;

        private readonly CompositeDisposable _disposable = new();

        public AttractEntryPoint(IEnumerable<IAttractSourceCore> attractables,
            IAttractAreaDetectionLogic attractAreaDetectionLogic,
            IAttractUpdateLogic attractUpdateLogic,
            IRotateUpdateLogic rotateUpdateLogic)
        {
            _attractables = attractables;
            _attractAreaDetectionLogic = attractAreaDetectionLogic;
            _attractUpdateLogic = attractUpdateLogic;
            _rotateUpdateLogic = rotateUpdateLogic;
        }

        public void Start()
        {
            foreach (var source in _attractables)
            {
                Register(source);
            }

            _attractables = null;
        }

        private void Register(IAttractSourceCore source)
        {
            source.OnAttractUpdate
                .Subscribe(_ =>
                {
                    _attractUpdateLogic.UpdateAttract(source);
                    _rotateUpdateLogic.UpdateRotate(source);
                })
                .AddTo(_disposable);

            source.OnEnterAttractArea
                .Subscribe(attractable =>
                {
                    _attractAreaDetectionLogic.OnSourceEnter(source, attractable);
                    _attractAreaDetectionLogic.DetectNearestSource(attractable);
                })
                .AddTo(_disposable);

            source.OnExitAttractArea
                .Subscribe(attractable =>
                {
                    _attractAreaDetectionLogic.OnSourceExit(source, attractable);
                    _attractAreaDetectionLogic.DetectNearestSource(attractable);
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}