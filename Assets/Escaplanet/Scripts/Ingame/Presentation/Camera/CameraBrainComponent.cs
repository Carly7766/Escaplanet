using Escaplanet.Ingame.Core.Camera;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

namespace Escaplanet.Ingame.Presentation.Camera
{
    public class CameraBrainComponent : MonoBehaviour, ICameraBrainCore
    {
        #region MonoBehaviour Fields

        private UnityEngine.Camera _camera;
        private Transform _transform;

        #endregion

        #region Interface Fields

        public CameraState CurrentState { get; set; }

        public IVirtualCameraCore ActiveCamera { get; set; }
        public IVirtualCameraCore PreviousCamera { get; set; }

        #endregion

        #region MonoBehaviour Functions

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _transform = GetComponent<Transform>();
            CurrentState = new CameraState(new Vector2(_transform.localPosition.x, _transform.localPosition.y),
                _transform.localRotation.z, _camera.orthographicSize);
        }

        #endregion

        #region Interface Functions

        public void ApplyCameraState(CameraState state)
        {
            _transform.localPosition = new Vector3(state.Position.X, state.Position.Y, -10);
            _transform.localRotation = Quaternion.Euler(0, 0, state.Rotation);
            _camera.orthographicSize = state.OrthographicSize;
        }

        #endregion
    }
}