using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.Core.Attract
{
    public interface IRotateAttractableCore : IAttractableCore
    {
        ScalarFloat Rotation { get; }

        ScalarFloat PreviousTargetRotation { get; set; }
        ScalarFloat AngularVelocity { get; set; }
        ScalarFloat SmoothTime { get; }
        ScalarFloat MaxRotateSpeed { get; }
        void Rotate(ScalarFloat angle);
    }
}