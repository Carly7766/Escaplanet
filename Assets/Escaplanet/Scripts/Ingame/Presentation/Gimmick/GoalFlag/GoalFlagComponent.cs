using System;
using Escaplanet.Ingame.Core.Gimmick.GoalFlag;
using Escaplanet.Ingame.Core.Player;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.Gimmick.GoalFlag
{
    public class GoalFlagComponent : MonoBehaviour, IGoalFlagCore
    {
        public Observable<Unit> OnGoalReached { get; private set; }

        private void Awake()
        {
            var _collider2D = GetComponent<Collider2D>();

            OnGoalReached = _collider2D
                .OnCollisionEnter2DAsObservable()
                .Where(c => c.gameObject.GetComponentInParent<IPlayerMovementCore>() != null)
                .Select(_ => Unit.Default)
                .TakeUntil(destroyCancellationToken);
        }
    }
}