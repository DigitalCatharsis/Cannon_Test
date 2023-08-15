using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class EnemyPoolObject: MonoBehaviour, IPoolObject
    {
        [Inject] private PoolManager _poolManager;
        public EnemyType poolObjectType;

        public void ReturnToPool()
        {
            if (!_poolManager.enemyPoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                _poolManager.AddObject(this);
            }
        }
    }
}
