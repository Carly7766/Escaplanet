namespace Escaplanet.Ingame.Core.Camera
{
    public interface ICameraBrainCore
    {
        CameraState CurrentState { get; set; }

        IVirtualCameraCore ActiveCamera { get; set; }
        IVirtualCameraCore PreviousCamera { get; set; }

        void ApplyCameraState(CameraState state);
    }
}