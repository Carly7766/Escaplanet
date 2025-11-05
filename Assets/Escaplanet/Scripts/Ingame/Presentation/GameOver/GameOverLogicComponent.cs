using Escaplanet.Ingame.Core.GameOver;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escaplanet.Ingame.Presentation.GameOver
{
    public class GameOverLogicComponent : MonoBehaviour, IGameOverLogicCore
    {
        public void GameOver()
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}