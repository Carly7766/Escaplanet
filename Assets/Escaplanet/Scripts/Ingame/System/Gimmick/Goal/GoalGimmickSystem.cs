using Escaplanet.Ingame.Data.Gimmick;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escaplanet.Ingame.System.Gimmick.Goal
{
    public class GoalGimmickSystem
    {
        private CompositeDisposable disposables = new();

        public void RegisterGoalGimmick(IGoalEntity goalEntity)
        {
            goalEntity.OnGoalReached.Subscribe(_ => OnGoal()).AddTo(disposables);
        }

        private void OnGoal()
        {
            Debug.Log("goal");
            SceneManager.LoadScene("Result");
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}