using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PowerUpPoolObject: MonoBehaviour, IPoolObject
    {
        [Inject] private PoolManager _poolManager;
        public PowerUpType poolObjectType;

        public void ReturnToPool()
        {
            if (!_poolManager.powerUpPoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                _poolManager.AddObject(this);
            }
        }
    }
}
