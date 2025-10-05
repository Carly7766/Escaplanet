using Escaplanet.Root.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private UnityGlobalValuePort unityGlobalValuePort;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(unityGlobalValuePort).AsImplementedInterfaces();
            builder.Register<UnityFloatMathPort>(Lifetime.Singleton).As<IFloatMathPort>();
        }
    }
}