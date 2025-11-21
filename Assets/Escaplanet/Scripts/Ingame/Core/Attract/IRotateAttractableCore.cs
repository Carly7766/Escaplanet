namespace Escaplanet.Ingame.Core.Attract
{
    public interface IRotateAttractableCore : IAttractableCore
    {
        float Rotation { get; }

        float PreviousTargetRotation { get; set; }
        float AngularVelocity { get; set; }
        float SmoothTime { get; }
        float MaxRotateSpeed { get; }
        void Rotate(float angle);
    }
}