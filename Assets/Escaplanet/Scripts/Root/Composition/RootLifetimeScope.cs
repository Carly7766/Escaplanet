using Escaplanet.Root.Core.Common;
using Escaplanet.Root.GameLogic;
using Escaplanet.Root.Presentation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Root.Composition
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameInfoScriptableObject gameInfoScriptableObject;
        [SerializeField] private UnityGlobalValuePort unityGlobalValuePort;
        [SerializeField] private UnitySceneLoadPort unitySceneLoadPort;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(unityGlobalValuePort).AsImplementedInterfaces();
            builder.Register<UnityFloatMathPort>(Lifetime.Singleton).As<IFloatMathPort>();
            builder.RegisterInstance(unitySceneLoadPort).As<ISceneLoadPort>();

            builder.RegisterInstance(gameInfoScriptableObject).AsImplementedInterfaces();

            builder.Register<SceneTransitionLogic>(Lifetime.Singleton);
            builder.Register<GameStateChangeLogic>(Lifetime.Singleton);
        }
    }
}