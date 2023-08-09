using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class EnemyPoolObject: MonoBehaviour, IPoolObject
    {
        [Inject] private PoolManager _poolManager;

        private EnemyControl _enemyControl;
        public EnemyType poolObjectType;

        private void Awake()
        {
            _enemyControl = this.gameObject.GetComponent<EnemyControl>();
        }
        public void GotKilled()
        {
            if (!_poolManager.enemyPoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
        public void TurnOff()
        {            
            _enemyControl.OnTurnOff();
            _poolManager.AddObject(this);
        }
    }
}
