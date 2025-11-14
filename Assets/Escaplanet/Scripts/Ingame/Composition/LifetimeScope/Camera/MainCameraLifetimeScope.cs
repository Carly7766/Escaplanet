using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.EntryPoint.Camera;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Camera
{
    public class MainCameraLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GetComponent<IMainCameraCore>()).AsSelf();
            builder.Register<MainCameraControlCore>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<MainCameraEntryPoint>();
        }
    }
}