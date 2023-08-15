using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectilePoolObject : MonoBehaviour, IPoolObject
    {
        [Inject] private PoolManager _poolManager;
        public ProjectileType poolObjectType;

        public void ReturnToPool()
        {
            if (!_poolManager.projectilePoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                _poolManager.AddObject(this);
            }
        }
    }
}
