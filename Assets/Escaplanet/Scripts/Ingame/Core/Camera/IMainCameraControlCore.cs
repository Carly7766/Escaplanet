namespace Escaplanet.Ingame.Core.Camera
{
    public interface IMainCameraControlCore
    {
        IVirtualCameraCore PlayerCamera { get; set; }
        IVirtualCameraCore WorldCamera { get; set; }
    }
}