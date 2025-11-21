using Escaplanet.Result.Core;
using Escaplanet.Result.EntryPoint;
using Escaplanet.Result.GameLogic;
using Escaplanet.Result.Presentation;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Result.Composition
{
    public class ResultLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GetComponent<IResultDisplayCore>());
            builder.RegisterInstance(GetComponent<IResultInputCore>());

            builder.Register<ResultDisplayLogic>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ResultEntryPoint>();
        }
    }
}