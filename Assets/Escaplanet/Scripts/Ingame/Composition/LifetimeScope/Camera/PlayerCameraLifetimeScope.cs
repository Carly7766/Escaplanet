using Escaplanet.Ingame.Core.Camera;
using Escaplanet.Ingame.EntryPoint.Camera;
using Escaplanet.Ingame.GameLogic.Camera.PlayerCamera;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Camera
{
    public class PlayerCameraLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GetComponent<IPlayerCameraCore>()).AsSelf();

            builder.RegisterEntryPoint<PlayerCameraEntryPoint>(Lifetime.Scoped);
        }
    }
}