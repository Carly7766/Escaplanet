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
        [SerializeField] private CountdownTextComponent countdownTextComponent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(GetComponent<IPlayerInputCore>()).AsSelf();
            builder.RegisterComponent(GetComponent<IPlayerMovementCore>()).AsSelf();
            builder.RegisterComponent(GetComponent<IGameOverDetectableCore>()).AsSelf();
            builder.RegisterComponent(countdownTextComponent).AsImplementedInterfaces();

            builder.RegisterEntryPoint<PlayerEntryPoint>(Lifetime.Scoped);
        }
    }
}