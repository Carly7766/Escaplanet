using Escaplanet.Ingame.Core.Camera;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.Camera
{
    public class CameraBackgroundComponent : MonoBehaviour, ICameraBackgroundCore
    {
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FitBackgroundToCamera(CameraState currentState, float mainCameraAspect)
        {
            _transform.position =
                new Vector3(currentState.Position.X, currentState.Position.Y, -currentState.Position.Z);
            _transform.localRotation = Quaternion.Euler(0, 0, currentState.Rotation);


            var cameraHeight = currentState.OrthographicSize * 2.0f;
            var cameraWidth = cameraHeight * mainCameraAspect;

            _transform.localScale = Vector3.one;
            _spriteRenderer.size = new Vector2(cameraWidth, cameraHeight);
        }
    }
}