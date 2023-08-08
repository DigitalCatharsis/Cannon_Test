using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class EnemyPoolObject: MonoBehaviour
    {
        [Inject] private PoolManager _poolManager;
        [Inject] private Spawner _spawner;

        public EnemyType poolObjectType;
        public void GotKilled()
        {
            if (!_poolManager.enemyPoolDictionary[poolObjectType].Contains(this.gameObject))
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
