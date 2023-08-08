using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PowerUpPoolObject: MonoBehaviour
    {
        [Inject] private PoolManager _poolManager;
        [Inject] private Spawner _spawner;

        public PowerUpType poolObjectType;
        public void GotKilled()
        {
            if (!_poolManager.powerUpPoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
        public void TurnOff()
        {
            this.transform.position = _spawner.GetRandomPosition();

            _poolManager.AddObject(this);
        }

    }
}
