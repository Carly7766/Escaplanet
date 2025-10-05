using System.Collections.Generic;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.Core.Attract
{
    public interface IReadonlyAttractSourceCore
    {
        Vector2 Position { get; }

        float SurfaceGravity { get; }
        float Radius { get; }

        bool IsDestroyed { get; }

        IReadOnlyCollection<IReadonlyAttractableCore> AttractablesInArea { get; }
    }

    public interface IAttractSourceCore : IReadonlyAttractSourceCore
    {
        void AddAttractableInArea(IReadonlyAttractableCore attractable);
        void RemoveAttractableInArea(IReadonlyAttractableCore attractable);
    }
}