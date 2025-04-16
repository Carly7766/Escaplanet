using UnityEngine;

namespace Escaplanet.Scripts.Test
{
    public enum RotationMethod
    {
        DirectRigidbodyRotation,
        MoveRotation,
        TransformRotate,
        DirectTransformRotation,
        SlerpTransform,
        SlerpRigidbody,
        RotateTowardsTransform,
        RotateTowardsRigidbody,
        AngularVelocity
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class MockPlayerMover : MonoBehaviour
    {
        [Header("Target")] [SerializeField] private Transform planetTransform;

        [Header("Rotation Settings")] [SerializeField]
        private RotationMethod rotationMethod = RotationMethod.MoveRotation;

        [SerializeField] private float rotateSpeed = 5f;

        [Header("Movement Settings")] [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField] private float acceleration = 50f;
        [SerializeField] private float movementLerpAmount = 1.0f;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _inputAxis;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // PlayerMoverから移植: 水平入力取得
            _inputAxis = new Vector2(Input.GetAxis("Horizontal"), 0);
        }

        private void FixedUpdate()
        {
            if (planetTransform == null) return;

            Move();
            Rotate();
        }

        private void Move()
        {
            var diff = _transform.position - planetTransform.position;

            var zDir = new Vector3(planetTransform.position.x, planetTransform.position.y, -1.0f);
            var perpendicular = Vector3.Cross(zDir, diff);
            var perpendicularNormalized = (Vector2)perpendicular.normalized;

            var perpendicularSpeed = Vector2.Dot(_rigidbody2D.velocity, perpendicularNormalized);

            var targetSpeed = _inputAxis.x * moveSpeed;
            targetSpeed = Mathf.Lerp(perpendicularSpeed, targetSpeed, movementLerpAmount);

            var accelRate = (acceleration / moveSpeed) * (1.0f / Time.fixedDeltaTime);

            var speedDif = targetSpeed - perpendicularSpeed;
            var movement = speedDif * accelRate;

            _rigidbody2D.AddForce(perpendicularNormalized * movement, ForceMode2D.Force);
        }

        private void Rotate()
        {
            var direction = ((Vector2)planetTransform.position - _rigidbody2D.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            var targetAngle = targetRotation.eulerAngles.z;

            switch (rotationMethod)
            {
                case RotationMethod.DirectRigidbodyRotation:
                    _rigidbody2D.rotation = targetAngle;
                    break;

                case RotationMethod.MoveRotation:
                    _rigidbody2D.MoveRotation(targetAngle);
                    break;

                case RotationMethod.TransformRotate:
                    var currentAngleTR = _transform.rotation.eulerAngles.z;
                    var deltaAngleTR = Mathf.DeltaAngle(currentAngleTR, targetAngle);
                    _transform.Rotate(0f, 0f, deltaAngleTR * rotateSpeed * Time.fixedDeltaTime);
                    break;

                case RotationMethod.DirectTransformRotation:
                    _transform.rotation = targetRotation;
                    break;

                case RotationMethod.SlerpTransform:
                    var currentRotationST = _transform.rotation;
                    _transform.rotation = Quaternion.Slerp(currentRotationST, targetRotation,
                        rotateSpeed * Time.fixedDeltaTime);
                    break;

                case RotationMethod.SlerpRigidbody:
                    var currentRotationSR = Quaternion.Euler(0, 0, _rigidbody2D.rotation);
                    var nextRotationSR = Quaternion.Slerp(currentRotationSR, targetRotation,
                        rotateSpeed * Time.fixedDeltaTime);
                    _rigidbody2D.MoveRotation(nextRotationSR.eulerAngles.z);
                    break;

                case RotationMethod.RotateTowardsTransform:
                    var currentRotationRTT = _transform.rotation;
                    float maxDegreesDeltaRTT = rotateSpeed * Time.fixedDeltaTime;
                    _transform.rotation =
                        Quaternion.RotateTowards(currentRotationRTT, targetRotation, maxDegreesDeltaRTT);
                    break;

                case RotationMethod.RotateTowardsRigidbody:
                    var currentRotationRTR = Quaternion.Euler(0, 0, _rigidbody2D.rotation);
                    float maxDegreesDeltaRTR = rotateSpeed * Time.fixedDeltaTime;
                    var nextRotationRTR =
                        Quaternion.RotateTowards(currentRotationRTR, targetRotation, maxDegreesDeltaRTR);
                    _rigidbody2D.MoveRotation(nextRotationRTR.eulerAngles.z);
                    break;

                case RotationMethod.AngularVelocity:
                    var currentAngleAV = _rigidbody2D.rotation;
                    var deltaAngleAV = Mathf.DeltaAngle(currentAngleAV, targetAngle);
                    _rigidbody2D.angularVelocity = deltaAngleAV * rotateSpeed;
                    break;
            }
        }
    }
}