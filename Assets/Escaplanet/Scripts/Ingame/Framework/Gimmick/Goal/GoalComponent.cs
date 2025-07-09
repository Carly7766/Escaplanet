using Escaplanet.Ingame.Data;
using Escaplanet.Ingame.Data.EntityId;
using Escaplanet.Ingame.Data.Gimmick;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Escaplanet.Ingame.Framework.Gimmick.Goal
{
    public class GoalComponent : MonoBehaviour, IGoalEntity
    {
        private void OnDestroy()
        {
            _onDestroySubject.OnNext(Id);
            _onDestroySubject.OnCompleted();
        }

        public void Initialize(EntityId id)
        {
            Id = id;
        }

        public EntityId Id { get; private set; }
        public bool IsActive => isActiveAndEnabled;
        public bool IsDestroyed => !this;
        private readonly Subject<EntityId> _onDestroySubject = new();
        Observable<EntityId> IEntity.OnDestroy => _onDestroySubject;

        public Observable<Unit> OnGoalReached => this.OnTriggerEnter2DAsObservable()
            .Where(collider => collider.CompareTag("Player"))
            .Select(_ => Unit.Default)
            .AsObservable();
    }
}