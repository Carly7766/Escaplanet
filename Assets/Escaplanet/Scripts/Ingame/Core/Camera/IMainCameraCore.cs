namespace Escaplanet.Ingame.Core.Camera
{
    public interface IMainCameraCore
    {
        CameraState CurrentState { get; set; }

        IVirtualCameraCore ActiveCamera { get; set; }
        IVirtualCameraCore PreviousCamera { get; set; }

        void ApplyCameraState(CameraState state);
    }
}