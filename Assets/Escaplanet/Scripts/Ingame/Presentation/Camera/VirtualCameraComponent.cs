using Escaplanet.Ingame.Core.Camera;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.Camera
{
    public class VirtualCameraComponent : MonoBehaviour, IVirtualCameraCore
    {
        #region Unity Fields

        private Transform _transform;
        [SerializeField] private float orthographicSize = 15f;

        #endregion

        #region Interface Fields

        public CameraState State
        {
            get => new(new Root.Common.ValueObject.Vector2(_transform.localPosition.x, _transform.localPosition.y),
                _transform.localRotation.z, orthographicSize);
            set
            {
                _transform.position = new UnityEngine.Vector2(value.Position.X, value.Position.Y);
                _transform.rotation = Quaternion.Euler(0f, 0f, value.Rotation);
                orthographicSize = value.OrthographicSize;
            }
        }

        #endregion

        #region MonoBehaviour Functions

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        #endregion
    }
}