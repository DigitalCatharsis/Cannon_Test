using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PowerUpPoolObject: MonoBehaviour, IPoolObject
    {
        [Inject] private PoolManager _poolManager;
        private PowerUp _powerUp;

        public PowerUpType poolObjectType;

        private void Awake()
        {
            _powerUp = this.gameObject.GetComponent<PowerUp>();
        }

        public void GotKilled()
        {
            if (!_poolManager.powerUpPoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
        public void TurnOff()
        {
            _powerUp.OnTurnOff();
            _poolManager.AddObject(this);
        }
    }
}
