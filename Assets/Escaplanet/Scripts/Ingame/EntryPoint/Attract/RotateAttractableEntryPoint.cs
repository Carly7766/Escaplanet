using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.GameLogic.Attract;
using UnityEngine;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Attract
{
    public class RotateAttractableEntryPoint : IFixedTickable
    {
        private readonly IRotateAttractableCore _rotateAttractable;
        private readonly IRotateUpdateLogic _rotateUpdateLogic;

        public RotateAttractableEntryPoint(IRotateAttractableCore rotateAttractable,
            IRotateUpdateLogic rotateUpdateLogic)
        {
            _rotateAttractable = rotateAttractable;
            _rotateUpdateLogic = rotateUpdateLogic;
        }

        public void FixedTick()
        {
            _rotateUpdateLogic.UpdateRotate(_rotateAttractable);
        }
    }
}