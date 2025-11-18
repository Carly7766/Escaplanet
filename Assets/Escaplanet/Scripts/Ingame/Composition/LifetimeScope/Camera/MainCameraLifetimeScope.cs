using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.EntryPoint.Camera;
using Escaplanet.Ingame.GameLogic.Camera;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Camera
{
    public class MainCameraLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GetComponent<IMainCameraCore>());
            builder.Register<MainCameraControlCore>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(GetComponentInChildren<ICameraBackgroundCore>());

            builder.Register<CameraBackgroundFitLogic>(Lifetime.Scoped);

            builder.RegisterEntryPoint<MainCameraEntryPoint>();
        }
    }
}