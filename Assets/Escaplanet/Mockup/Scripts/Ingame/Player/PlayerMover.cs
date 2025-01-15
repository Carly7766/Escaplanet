using UnityEngine;

namespace Escaplanet.Mockup.Ingame.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Transform planetTransform;
        [SerializeField] private float speed = 5f;

        [SerializeField] private float acceleration = 5f;
        [SerializeField] private float lerpAmount = 1.0f;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float jumpForce = 5f;

        private Vector2 _inputAxis;
        private bool _pressedJump;
        private bool _isGrounded;

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _inputAxis = new Vector2(Input.GetAxis("Horizontal"), 0);
            _pressedJump = Input.GetButtonDown("Jump");


            var ray = new Ray2D(transform.position, -transform.up);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, 1.05f, groundLayerMask);

            Debug.DrawRay(ray.origin, ray.direction * 1.05f, Color.red); // 長さ1f、緑色で1フレーム可視化

            if (hit.collider)
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }

        private void FixedUpdate()
        {
            var diff = _transform.position - planetTransform.position;
            var zDir = new Vector3(planetTransform.position.x,
                planetTransform.position.y, -1.0f);
            var perpendicular = Vector3.Cross(zDir, diff);
            var perpendicularNormalized = perpendicular.normalized;

            var perpendicularSpeed = Vector2.Dot(_rigidbody2D.velocity, perpendicularNormalized);

            var targetSpeed = _inputAxis.x * speed;
            targetSpeed = Mathf.Lerp(perpendicularSpeed, targetSpeed, lerpAmount);

            var accelRate = (acceleration / speed) * (1.0f / Time.fixedDeltaTime);

            var speedDif = targetSpeed - perpendicularSpeed;
            var movement = speedDif * accelRate;
            _rigidbody2D.AddForce(perpendicularNormalized * movement, ForceMode2D.Force);

            if (_isGrounded && _pressedJump)
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _isGrounded = false;
            }
        }
    }
}