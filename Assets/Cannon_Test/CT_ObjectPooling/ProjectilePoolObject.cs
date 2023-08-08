using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectilePoolObject : MonoBehaviour, IPoolObject
    {
        [Inject] private PoolManager _poolManager;
        [Inject] private LevelSpawner _spawner;

        public ProjectileType poolObjectType;
        public void GotKilled()
        {
            if (!_poolManager.projectilePoolDictionary[poolObjectType].Contains(this.gameObject))
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
