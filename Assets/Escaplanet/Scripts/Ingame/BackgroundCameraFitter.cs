using System;
using UnityEngine;

namespace Escaplanet.Escaplanet.Scripts.Ingame
{
    public class BackgroundCameraFitter : MonoBehaviour
    {
        private int _height;
        private int _width;
        private float _size;

        private SpriteRenderer _spriteRenderer;
        private Transform _transform;

        [SerializeField] private new Camera camera;
        private Transform _cameraTransform;

        private float CameraDistance => Mathf.Abs(_transform.position.z - _cameraTransform.position.z);

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = transform;

            if (camera == null)
                camera = Camera.main;
            _cameraTransform = camera?.transform;
        }

        private void Update()
        {
            FollowCamera();

            var tempSize = camera.orthographicSize;
            var tempWidth = Screen.width;
            var tempHeight = Screen.height;

            if (Mathf.Approximately(_size, tempSize) && _width == tempWidth && _height == tempHeight)
                return;
            _size = tempSize;
            _width = tempWidth;
            _height = tempHeight;

            switch (_spriteRenderer.drawMode)
            {
                case SpriteDrawMode.Simple:
                    FitImageSimple();
                    break;
                case SpriteDrawMode.Sliced:
                case SpriteDrawMode.Tiled:
                    FitImageTiled();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FollowCamera()
        {
            var cameraRotation = _cameraTransform.rotation;
            var targetPosition = _cameraTransform.position + cameraRotation * Vector3.forward * CameraDistance;

            _transform.position = targetPosition;
            _transform.rotation = cameraRotation;
        }

        private void FitImageSimple()
        {
            var worldScreenHeight = camera.orthographicSize * 2;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            var width = _spriteRenderer.sprite.bounds.size.x;
            var height = _spriteRenderer.sprite.bounds.size.y;

            _transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
        }

        private void FitImageTiled()
        {
            var worldScreenHeight = camera.orthographicSize * 2;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            _transform.localScale = Vector3.one;
            _spriteRenderer.size = new Vector2(worldScreenWidth, worldScreenHeight);
        }
    }
}