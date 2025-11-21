using Escaplanet.Ingame.Core.Camera;
using UnityEngine;
using Vector3 = Escaplanet.Root.Common.ValueObject.Vector3;

namespace Escaplanet.Ingame.Presentation.Camera
{
    public class VirtualCameraComponent : MonoBehaviour, IVirtualCameraCore
    {
        #region MonoBehaviour Functions

        protected virtual void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        #endregion

        #region Interface Fields

        public CameraState State
        {
            get => new(
                new Vector3(_transform.localPosition.x, _transform.localPosition.y,
                    _transform.localPosition.z),
                _transform.localRotation.z, orthographicSize);
            set
            {
                _transform.position = new UnityEngine.Vector3(value.Position.X, value.Position.Y, value.Position.Z);
                _transform.rotation = Quaternion.Euler(0f, 0f, value.Rotation);
                orthographicSize = value.OrthographicSize;
            }
        }

        #endregion

        #region Unity Fields

        private Transform _transform;
        [SerializeField] protected float orthographicSize = 15f;

        #endregion
    }
}