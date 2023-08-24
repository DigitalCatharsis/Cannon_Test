using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PowerUpControl : MonoBehaviour, IPooledHittingObject
    {
        public PowerUpType _powerUpType;

        [Inject] private LevelSpawner _levelSpawner;

        private void Awake()
        {
            _powerUpType = GetComponent<PowerUpPoolObject>().poolObjectType;
        }

        private void Update()
        {
            this.transform.Rotate(0, 0.5f, 0);
        }

        public void OnEnable()
        {
        }

        public void OnDisable()
        {
            this.transform.position = _levelSpawner.GetRandomPosition(_levelSpawner.minCoordinates,_levelSpawner.maxCoordinates);
        }
    }
}
