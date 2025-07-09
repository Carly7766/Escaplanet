using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escaplanet.Test.Mock
{
    public class MockResultToIngame : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Ingame");
            }
        }
    }
}