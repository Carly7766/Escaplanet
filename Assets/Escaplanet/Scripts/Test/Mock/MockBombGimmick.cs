using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Escaplanet.Test.Mock
{
    public class MockBombGimmick : MonoBehaviour
    {
        [SerializeField] private float blinkInterval = 0.1f;
        [SerializeField] private float explodeTime = 3f;
        [SerializeField] private float explodeRadius;
        [SerializeField] private float explodeForce = 5f;
        [SerializeField] private LayerMask explodeLayerMask;
        private readonly Color _bombBlinkColor = new(1f, 0.5f, 0.5f);

        private readonly Color _bombColor = Color.gray;
        private SpriteRenderer _explodeSpriteRenderer;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _explodeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            transform.GetChild(0).localScale = new Vector3(
                transform.localScale.x * explodeRadius,
                transform.localScale.y * explodeRadius, 1);
        }

        private void Start()
        {
            var token = destroyCancellationToken;
            Detonate(token);
        }

        private async void Detonate(CancellationToken cancellationToken = default)
        {
            var blinkCancellationTokenSource = new CancellationTokenSource();
            var blinkCancellationToken = CancellationTokenSource
                .CreateLinkedTokenSource(cancellationToken, blinkCancellationTokenSource.Token).Token;

            Blink(blinkCancellationToken).Forget();

            try
            {
                await UniTask.WaitForSeconds(explodeTime, cancellationToken: cancellationToken);
                blinkCancellationTokenSource.Cancel();

                _explodeSpriteRenderer.enabled = true;
                var targetColliders = Physics2D.OverlapCircleAll(transform.position,
                    transform.localScale.x * explodeRadius / 2, explodeLayerMask);
                foreach (var targetCollider in targetColliders) ApplyExplosionForce(targetCollider);

                await UniTask.WaitForSeconds(0.5f, cancellationToken: cancellationToken);
                Destroy(gameObject);
            }
            catch (OperationCanceledException)
            {
                blinkCancellationTokenSource.Cancel();
            }
            finally
            {
                blinkCancellationTokenSource.Dispose();
            }
        }

        private async UniTask Blink(CancellationToken token = default)
        {
            var blinkCount = 0;
            while (!token.IsCancellationRequested)
            {
                _spriteRenderer.color = blinkCount % 2 == 0 ? _bombBlinkColor : _bombColor;
                await UniTask.WaitForSeconds(blinkInterval, cancellationToken: token);
                blinkCount++;
            }
        }

        private void ApplyExplosionForce(Collider2D targetCollider)
        {
            var targetRigidbody = targetCollider.GetComponent<Rigidbody2D>();

            if (targetRigidbody != null)
            {
                Vector2 explosionDirection = targetCollider.transform.position - transform.position;
                var distance = explosionDirection.magnitude;
                var normalizedDistance = distance / explodeRadius;
                var force = Mathf.Lerp(explodeForce, 0f, normalizedDistance);

                targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode2D.Impulse);
            }
        }
    }
}