using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escaplanet.Escaplanet.Scenes
{
    public class SceneTransitionMock : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(_sceneName);
            }
        }
    }
}