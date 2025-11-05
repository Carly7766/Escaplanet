using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escaplanet.Escaplanet.Scenes
{
    public class BackToIngameMok : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Ingame");
            }
        }
    }
}