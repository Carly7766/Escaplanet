using Escaplanet.Root.Core;
using Escaplanet.Root.Core.Common;
using UnityEngine;

namespace Escaplanet.Root.Presentation
{
    public class UnitySceneLoadPort : MonoBehaviour, ISceneLoadPort
    {
        public void LoadScene(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Title:
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
                    break;
                case GameState.Ingame:
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Ingame");
                    break;
                case GameState.GameOver:
                    UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
                    break;
                case GameState.None:
                default:
                    Debug.LogWarning("Unsupported GameState: " + gameState);
                    break;
            }
        }
    }
}