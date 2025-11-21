using Escaplanet.Root.Core;
using Escaplanet.Root.Core.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escaplanet.Root.Presentation
{
    public class UnitySceneLoadPort : MonoBehaviour, ISceneLoadPort
    {
        public void LoadScene(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Title:
                    SceneManager.LoadScene("Title");
                    break;
                case GameState.Ingame:
                    SceneManager.LoadScene("Ingame");
                    break;
                case GameState.GameOver:
                    SceneManager.LoadScene("GameOver");
                    break;
                case GameState.None:
                default:
                    Debug.LogWarning("Unsupported GameState: " + gameState);
                    break;
            }
        }
    }
}