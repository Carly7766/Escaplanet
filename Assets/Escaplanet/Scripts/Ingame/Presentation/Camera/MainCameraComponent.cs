using Escaplanet.Ingame.Core.Camera;
using UnityEngine;
using Vector3 = Escaplanet.Root.Common.ValueObject.Vector3;

namespace Escaplanet.Ingame.Presentation.Camera
{
    public class MainCameraComponent : MonoBehaviour, IMainCameraCore
    {
        #region MonoBehaviour Fields

        private UnityEngine.Camera _camera;
        private Transform _transform;
        [SerializeField] private float transitionDuration = 2f;

        #endregion

        #region Interface Fields

        public CameraState CurrentState => new(
            new Vector3(_transform.localPosition.x, _transform.localPosition.y, _transform.position.z),
            _transform.localRotation.eulerAngles.z,
            _camera.orthographicSize);

        public float Aspect => _camera.aspect;

        public IVirtualCameraCore ActiveCamera { get; set; }
        public IVirtualCameraCore PreviousCamera { get; set; }

        public bool IsTransitioning { get; set; }
        public float TransitionDuration => transitionDuration;
        public float TransitionTimer { get; set; }

        #endregion  

        #region MonoBehaviour Functions

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _transform = GetComponent<Transform>();
        }

        #endregion

        #region Interface Functions

        public void ApplyCameraState(CameraState state)
        {
            _transform.localPosition = new UnityEngine.Vector3(state.Position.X, state.Position.Y, state.Position.Z);
            _transform.localRotation = Quaternion.Euler(0, 0, state.Rotation);
            _camera.orthographicSize = state.OrthographicSize;
        }

        #endregion
    }
}