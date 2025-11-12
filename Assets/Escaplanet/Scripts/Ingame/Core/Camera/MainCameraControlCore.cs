namespace Escaplanet.Ingame.Core.Camera
{
    public class MainCameraControlCore : IMainCameraControlCore
    {
        public IVirtualCameraCore PlayerCamera { get; set; }
        public IVirtualCameraCore WorldCamera { get; set; }
    }
}