using Escaplanet.Result.Core;
using UnityEngine;

namespace Escaplanet.Result.Presentation
{
    public class ResultDisplayComponent : MonoBehaviour, IResultDisplayCore
    {
        [SerializeField] private GameObject noneScope;
        [SerializeField] private GameObject gameOverScope;
        [SerializeField] private GameObject clearScope;

        public void DisplayNone()
        {
            noneScope.SetActive(true);
            gameOverScope.SetActive(false);
            clearScope.SetActive(false);
        }

        public void DisplayGameOver()
        {
            noneScope.SetActive(false);
            gameOverScope.SetActive(true);
            clearScope.SetActive(false);
        }

        public void DisplayClear()
        {
            noneScope.SetActive(false);
            gameOverScope.SetActive(false);
            clearScope.SetActive(true);
        }
    }
}