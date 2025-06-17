using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Framework.Attract;
using Escaplanet.Ingame.Framework.Player;
using Escaplanet.Ingame.System.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Ingame.Composition.Player
{
    public class PlayerLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameObject playerGameObject;

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(playerGameObject.GetComponent<AttractableComponent>())
                .AsImplementedInterfaces();

            builder
                .RegisterInstance(playerGameObject.GetComponent<PlayerInputComponent>())
                .AsImplementedInterfaces();

            builder
                .RegisterInstance(playerGameObject.GetComponent<PlayerMoveComponent>())
                .AsImplementedInterfaces();

            builder.RegisterEntryPoint<PlayerMoveSystem>(Lifetime.Scoped);
        }
    }
}