using Escaplanet.Root.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private UnityGlobalPort globalPort;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(globalPort).AsImplementedInterfaces();
        }
    }
}