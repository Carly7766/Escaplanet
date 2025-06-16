using System;
using Escaplanet.Ingame.Data.Attract;
using UnityEngine;
using VContainer;

namespace Escaplanet.Test.Mock
{
    public class MockBombSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bombPrefab;

        private Camera _mainCamera;

        private Func<GameObject, Vector2, IAttractableEntity> attractableEntityFactory;

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
                worldPosition.z = 0;

                SpawnBomb(worldPosition);
            }
        }

        [Inject]
        private void Construct(Func<GameObject, Vector2, IAttractableEntity> attractableEntityFactory)
        {
            this.attractableEntityFactory = attractableEntityFactory;
        }

        private void SpawnBomb(Vector3 position)
        {
            attractableEntityFactory.Invoke(bombPrefab, position);
        }
    }
}