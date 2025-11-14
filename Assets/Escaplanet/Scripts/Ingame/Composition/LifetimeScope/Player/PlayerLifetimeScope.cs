using Escaplanet.Ingame.Core.GameOver;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.EntryPoint.Player;
using Escaplanet.Ingame.Presentation.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.LifetimeScope.Player
{
    public class PlayerLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(GetComponentInChildren<IPlayerInputCore>()).AsSelf();
            builder.RegisterComponent(GetComponentInChildren<IPlayerMovementCore>()).AsSelf();
            builder.RegisterComponent(GetComponentInChildren<IGameOverDetectableCore>()).AsSelf();
            builder.RegisterComponent(GetComponentInChildren<CountdownTextComponent>()).AsImplementedInterfaces();

            builder.RegisterEntryPoint<PlayerEntryPoint>(Lifetime.Scoped);
        }
    }
}