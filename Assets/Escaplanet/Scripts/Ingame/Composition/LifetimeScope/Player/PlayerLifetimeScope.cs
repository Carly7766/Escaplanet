using Escaplanet.Ingame.Core.GameOver;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.EntryPoint.Player;
using Escaplanet.Ingame.Presentation.UI;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Player
{
    public class PlayerLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(GetComponentInChildren<IPlayerInputCore>());
            builder.RegisterComponent(GetComponentInChildren<IPlayerMovementCore>());
            builder.RegisterComponent(GetComponentInChildren<IPlayerAppearanceCore>());
            builder.RegisterComponent(GetComponentInChildren<IGameOverDetectableCore>());
            builder.RegisterComponent(GetComponentInChildren<CountdownTextComponent>()).AsImplementedInterfaces();

            builder.RegisterEntryPoint<PlayerEntryPoint>(Lifetime.Scoped);
        }
    }
}