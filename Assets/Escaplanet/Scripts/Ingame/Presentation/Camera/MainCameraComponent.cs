using System;
using Escaplanet.Ingame.Core.Camera;
using LitMotion;
using UnityEngine;
using Vector2 = Escaplanet.Root.Common.ValueObject.Vector2;

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

        public CameraState CurrentState => new CameraState(
            new Vector2(_transform.localPosition.x, _transform.localPosition.y),
            _transform.localRotation.eulerAngles.z,
            _camera.orthographicSize);

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
            _transform.localPosition = new Vector3(state.Position.X, state.Position.Y, -10);
            _transform.localRotation = Quaternion.Euler(0, 0, state.Rotation);
            _camera.orthographicSize = state.OrthographicSize;
        }

        #endregion
    }
}