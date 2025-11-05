using System.Threading;
using Cysharp.Threading.Tasks;

namespace Escaplanet.Ingame.Core.UI
{
    public interface ICountdownTextCore
    {
        void Show();
        void Hide();
        UniTask RunCountdownAsync(int seconds, CancellationToken token = default);
    }
}