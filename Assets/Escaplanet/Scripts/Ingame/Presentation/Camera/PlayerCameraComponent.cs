using Escaplanet.Ingame.Core.Camera;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.Camera
{
    public class PlayerCameraComponent : MonoBehaviour, IPlayerCameraCore
    {
        #region Unity Fields

        private Transform _transform;
        [SerializeField] private float orthographicSize = 15f;

        #endregion

        #region Interface Fields

        public CameraState State
        {
            get => new(
                new Root.Common.ValueObject.Vector3(_transform.localPosition.x, _transform.localPosition.y,
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

        #region MonoBehaviour Functions

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        #endregion
    }
}