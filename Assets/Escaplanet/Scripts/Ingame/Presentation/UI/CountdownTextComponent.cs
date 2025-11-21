using System.Threading;
using Cysharp.Threading.Tasks;
using Escaplanet.Ingame.Core.UI;
using LitMotion;
using TMPro;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.UI
{
    public class CountdownTextComponent : MonoBehaviour, ICountdownTextCore
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _text.enabled = false;
        }

        public void Show()
        {
            _text.enabled = true;
        }

        public void Hide()
        {
            _text.enabled = false;
        }

        public async UniTask RunCountdownAsync(int seconds, CancellationToken token = default)
        {
            while (!token.IsCancellationRequested)
            {
                _text.text = seconds.ToString();
                await LSequence.Create()
                    .Join(LMotion.Create(360f, 240f, 1)
                        .WithEase(Ease.InSine)
                        .Bind(s => _text.fontSize = s))
                    .Join(LMotion.Create(0f, 1f, 1)
                        .WithEase(Ease.InSine)
                        .Bind(a => _text.alpha = a))
                    .Run()
                    .ToUniTask(token);

                seconds--;
                if (seconds <= 0) break;
            }
        }
    }
}