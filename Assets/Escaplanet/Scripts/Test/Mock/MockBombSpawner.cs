using System;
using UnityEngine;

namespace Escaplanet.Scripts.Test.Mock
{
    public class MockBombSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bombPrefab;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Input.mousePosition;
                var worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
                worldPosition.z = 0; // Set z to 0 for 2D

                SpawnBomb(worldPosition);
            }
        }

        private void SpawnBomb(Vector3 position)
        {
            Instantiate(bombPrefab, position, Quaternion.identity);
        }
    }
}