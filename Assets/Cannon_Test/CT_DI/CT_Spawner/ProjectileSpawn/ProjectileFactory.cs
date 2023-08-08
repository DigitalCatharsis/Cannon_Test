using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectileFactory : ICoreFactory<PowerUpType> //IEnemyFactory
    {
        [Inject] private DiContainer _diContainer;

        private Object _blowPrefab;
        private Object _freezeTimerPrefab;

        private const string _blowName = "Blow";
        private const string _freezeTimerName = "FreezeTimer";

        public ProjectileFactory()
        {
            _blowPrefab = Resources.Load(_blowName) as GameObject;
            _freezeTimerPrefab = Resources.Load(_freezeTimerName) as GameObject;
        }

        public GameObject AddToPool(PowerUpType enemyType, Vector3 position, Quaternion rotation)
        {
            switch (enemyType)
            {
                case PowerUpType.Blow:
                    {
                        return _diContainer.InstantiatePrefab(_blowPrefab, position, rotation, null);
                    }
                case PowerUpType.FreezeTimer:
                    {

                        return _diContainer.InstantiatePrefab(_freezeTimerPrefab, position, rotation, null);
                    }
                default:  //Интересно, как заткнуть эту дыру грамотно....
                    {
                        return null;
                    }
            }
        }
    }
}
