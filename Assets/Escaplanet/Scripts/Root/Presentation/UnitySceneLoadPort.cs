using Escaplanet.Root.Core;
using Escaplanet.Root.Core.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escaplanet.Root.Presentation
{
    public class UnitySceneLoadPort : MonoBehaviour, ISceneLoadPort
    {
        public void LoadTitleScene()
        {
            SceneManager.LoadScene("Title");
        }

        public void LoadIngameScene()
        {
            SceneManager.LoadScene("Ingame");
        }

        public void LoadResultScene()
        {
            SceneManager.LoadScene("Result");
        }
    }
}