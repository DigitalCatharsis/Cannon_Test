using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class EnemyFactory : IEnemyFactory
    {
        [Inject]
        private DiContainer _diContainer;

        private Object _zombieGeneralPrefab;
        private Object _zombieElitePrefab;

        private const string _zombieGeneralName = "ZombieElite";
        private const string _zombieEliteName = "ZombieGeneral";

        public EnemyFactory()
        {
            _zombieGeneralPrefab = Resources.Load(_zombieGeneralName) as GameObject;
            _zombieElitePrefab = Resources.Load(_zombieEliteName) as GameObject;
        }

        public void Create(EnemyType enemyType, Vector3 position)
        {
            switch (enemyType)
            {
                case EnemyType.ZOMBIE_ELITE:
                    {
                        _diContainer.InstantiatePrefab(_zombieElitePrefab, position, Quaternion.identity, null);
                        break;
                    }
                case EnemyType.ZOMBIE_GENERAL:
                    {
                        _diContainer.InstantiatePrefab(_zombieGeneralPrefab, position, Quaternion.identity, null);
                        break;
                    }
            }
        }
    }

}
