using Escaplanet.Escaplanet.Title.EntryPoint;
using Escaplanet.Escaplanet.Title.Presentation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Escaplanet.Title.Composition
{
    public class TitleLifetimeScope : LifetimeScope
    {
        [SerializeField] TitleInputComponent titleInputComponent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(titleInputComponent).AsImplementedInterfaces();

            builder.RegisterEntryPoint<TitleEntryPoint>(Lifetime.Singleton);
        }
    }
}