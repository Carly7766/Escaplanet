namespace Escaplanet.Ingame.Core.Camera
{
    public interface IMainCameraCore
    {
        CameraState CurrentState { get; }

        IVirtualCameraCore ActiveCamera { get; set; }
        IVirtualCameraCore PreviousCamera { get; set; }

        bool IsTransitioning { get; set; }
        float TransitionDuration { get; }
        float TransitionTimer { get; set; }

        void ApplyCameraState(CameraState state);
    }
}