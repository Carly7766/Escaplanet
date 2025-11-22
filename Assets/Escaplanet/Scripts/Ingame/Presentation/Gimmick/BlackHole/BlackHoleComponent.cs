using Escaplanet.Ingame.Core.Gimmick.BlackHole;
using Escaplanet.Ingame.Core.Player;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.Gimmick.BlackHole
{
    public class BlackHoleComponent : MonoBehaviour, IBlackHoleCore
    {
        public Observable<Unit> OnTouchPlayer { get; private set; }

        private void Awake()
        {
            var collider2D = GetComponent<Collider2D>();

            OnTouchPlayer = collider2D.OnTriggerEnter2DAsObservable()
                .Where(other => other.GetComponent<IPlayerMovementCore>() != null)
                .AsUnitObservable();
        }
    }
}