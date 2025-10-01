using System.Linq;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.EntryPoint.Attract;
using Escaplanet.Ingame.GameLogic.Attract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope
{
    public class AttractableLifetimeScope : VContainer.Unity.LifetimeScope
    {
        private IAttractableCore _attractable;

        protected override void Awake()
        {
            _attractable = GetComponent<IAttractableCore>();
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_attractable);
            builder.RegisterEntryPoint<AttractableEntryPoint>(Lifetime.Scoped);
        }
    }
}