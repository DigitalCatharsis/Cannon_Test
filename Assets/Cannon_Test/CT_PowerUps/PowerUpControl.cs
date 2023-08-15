using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PowerUpControl : MonoBehaviour, IPooledHittingObject
    {
        public PowerUpType _powerUpType;

        public delegate void PowerUpHandler(PowerUpControl sender, PowerUpEventArgs powerUpEventArgs);
        public event PowerUpHandler? NotifyPowerUpManager;

        [Inject] private LevelSpawner _spawner;
        [Inject] private PowerUpManager _powerUpManager;

        private void Awake()
        {
            _powerUpType = GetComponent<PowerUpPoolObject>().poolObjectType;
        }

        private void Update()
        {
            this.transform.Rotate(0, 0.5f, 0);
        }

        public void InvokePowerUp()
        {
            NotifyPowerUpManager(this, new PowerUpEventArgs(_powerUpType));
        }

        public void OnEnable()
        {
            _powerUpManager.SubscribeToPowerUp(this);
        }

        public void OnDisable()
        {
            _powerUpManager.UnsubscribeFromPowerUp(this);
            this.transform.position = _spawner.GetRandomPosition();
        }
    }

    public class PowerUpEventArgs
    {
        public PowerUpType _powerUpType;
        public PowerUpEventArgs(PowerUpType powerUpType)
        {
            _powerUpType = powerUpType;
        }
    }
}
