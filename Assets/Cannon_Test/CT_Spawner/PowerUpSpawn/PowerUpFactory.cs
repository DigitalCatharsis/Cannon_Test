using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PowerUpFactory : ICoreFactory<PowerUpType> //IEnemyFactory
    {

        [Inject] private DiContainer _diContainer;

        private readonly Object _blowPrefab;
        private readonly Object _freezeTimerPrefab;
        private readonly Object _heavyMachineGunPrefab;

        private readonly string _blowName;
        private readonly string _freezeTimerName;
        private readonly string _heavyMachineGunName;

        public PowerUpFactory()
        {
            _blowName = PowerUpType.Blow.ToString();
            _blowPrefab = Resources.Load(_blowName) as GameObject;

            _freezeTimerName = PowerUpType.FreezeTimer.ToString();
            _freezeTimerPrefab = Resources.Load(_freezeTimerName) as GameObject;

            _heavyMachineGunName = PowerUpType.HeavyMachineGun.ToString();
            _heavyMachineGunPrefab = Resources.Load(_heavyMachineGunName) as GameObject;
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
                case PowerUpType.HeavyMachineGun:
                    {

                        return _diContainer.InstantiatePrefab(_heavyMachineGunPrefab, position, rotation, null);
                    }
                default:  //Интересно, как заткнуть эту дыру грамотно....
                    {
                        return null;
                    }
            }
        }
    }
}
