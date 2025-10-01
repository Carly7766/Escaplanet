using System.Collections.Generic;
using Escaplanet.Root.Common.ValueObject;
using R3;

namespace Escaplanet.Ingame.Core.Attract
{
    public interface IReadonlyAttractableCore
    {
        Vector2 Position { get; }
        ScalarFloat Mass { get; }

        IReadOnlyCollection<IReadonlyAttractSourceCore> AffectingSources { get; }
        IReadonlyAttractSourceCore NearestSource { get; }

        Observable<IAttractSourceCore> OnEnterAttractArea { get; }
        Observable<IAttractSourceCore> OnExitAttractArea { get; }

        void Attract(Vector2 force);
    }

    public interface IAttractableCore : IReadonlyAttractableCore
    {
        void AddAffectingSource(IReadonlyAttractSourceCore source);
        void RemoveAffectingSource(IReadonlyAttractSourceCore source);
    }
}