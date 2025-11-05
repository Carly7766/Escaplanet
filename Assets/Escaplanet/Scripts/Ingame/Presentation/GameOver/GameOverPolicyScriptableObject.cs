using Escaplanet.Ingame.Core.GameOver;
using UnityEngine;

namespace Escaplanet.Ingame.Presentation.GameOver
{
    [CreateAssetMenu(fileName = "GameOverPolicy", menuName = "Escaplanet/GameOver/GameOverPolicy")]
    public class GameOverPolicyScriptableObject : ScriptableObject, IGameOverPolicy
    {
        [SerializeField] private float graceSeconds;
        [SerializeField] private int countdownSeconds;

        public float GraceSeconds => graceSeconds;
        public int CountdownSeconds => countdownSeconds;
    }
}