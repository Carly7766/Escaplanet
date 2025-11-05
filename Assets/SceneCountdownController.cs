using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCountdownController : MonoBehaviour
{
    [SerializeField] private bool flag = true;
    [SerializeField] private TMP_Text countdownText;

    public CancellationTokenSource _globalToken;


    private async void Start()
    {
        _globalToken = new CancellationTokenSource();

        LoopAsync(_globalToken.Token).Forget();
    }

    private async UniTask LoopAsync(CancellationToken token = default)
    {
        while (!token.IsCancellationRequested)
        {
            using var animationTokenSource = new CancellationTokenSource();
            var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token, animationTokenSource.Token);

            await UniTask.WaitUntil(() => !flag, cancellationToken: linkedTokenSource.Token);
            ObserveTrueAndCancelAsync(animationTokenSource).Forget();

            try
            {
                await RunCountdownAsync(5, linkedTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                SetCountdownVisible(false);
                continue;
            }
        }
    }

    private async UniTaskVoid ObserveTrueAndCancelAsync(CancellationTokenSource cts)
    {
        try
        {
            while (!cts.IsCancellationRequested && !flag)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cts.Token);
            }

            if (!cts.IsCancellationRequested && flag)
            {
                cts.Cancel();
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    private async UniTask RunCountdownAsync(int seconds, CancellationToken token = default)
    {
        var sec = seconds + 1;
        countdownText.text = sec.ToString();
        SetCountdownVisible(true);
        while (!token.IsCancellationRequested)
        {
            sec--;
            if (sec < 0) break;

            countdownText.text = sec.ToString();
            await LSequence.Create()
                .Join(LMotion.Create(360f, 240f, 1)
                    .WithEase(Ease.InSine)
                    .Bind(s => countdownText.fontSize = s))
                .Join(LMotion.Create(0f, 1f, 1)
                    .WithEase(Ease.InSine)
                    .Bind(a => countdownText.alpha = a))
                .Run()
                .ToUniTask(token);
        }
    }

    private void SetCountdownVisible(bool visible)
    {
        countdownText.gameObject.SetActive(visible);
    }

    private void OnDestroy()
    {
        _globalToken?.Cancel();
        _globalToken?.Dispose();
        _globalToken = null;
    }
}