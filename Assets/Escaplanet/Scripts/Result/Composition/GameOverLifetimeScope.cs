using Escaplanet.Result.EntryPoint;
using VContainer;
using VContainer.Unity;

namespace Escaplanet.Result.Composition
{
    public class GameOverLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameOverEntryPoint>(Lifetime.Singleton);
        }
    }
}