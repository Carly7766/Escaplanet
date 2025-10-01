using System.Collections.Generic;
using Escaplanet.Root.Common.ValueObject;
using R3;

namespace Escaplanet.Ingame.Core.Attract
{
    public interface IReadonlyAttractSourceCore
    {
        Vector2 Position { get; }

        ScalarFloat GravityConstant { get; }
        ScalarFloat SurfaceGravity { get; }
        ScalarFloat Radius { get; }

        IReadOnlyCollection<IReadonlyAttractableCore> AttractablesInArea { get; }
    }

    public interface IAttractSourceCore : IReadonlyAttractSourceCore
    {
        void AddAttractableInArea(IReadonlyAttractableCore attractable);
        void RemoveAttractableInArea(IReadonlyAttractableCore attractable);
    }
}