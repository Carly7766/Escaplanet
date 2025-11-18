namespace Escaplanet.Ingame.Core.Camera
{
    public interface ICameraBackgroundCore
    {
        void FitBackgroundToCamera(CameraState currentState, float mainCameraAspect);
    }
}