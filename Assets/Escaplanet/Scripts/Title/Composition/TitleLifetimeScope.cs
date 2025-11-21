using Escaplanet.Title.EntryPoint;
using Escaplanet.Title.Presentation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Title.Composition
{
    public class TitleLifetimeScope : LifetimeScope
    {
        [SerializeField] private TitleInputComponent titleInputComponent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(titleInputComponent).AsImplementedInterfaces();

            builder.RegisterEntryPoint<TitleEntryPoint>();
        }
    }
}