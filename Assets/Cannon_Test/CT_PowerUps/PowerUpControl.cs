using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PowerUpControl : MonoBehaviour, IPooledHittingObject
    {
        public PowerUpType _powerUpType;

        //public delegate void PowerUpHandler(PowerUpControl sender, PowerUpType _powerUpType);
        //public event PowerUpHandler? OnInvokePowerUp;

        [Inject] private LevelSpawner _levelSpawner;
        [Inject] private PowerUpManager _powerUpManager;
        [Inject] private SoundManager _soundManager;

        private void Awake()
        {
            _powerUpType = GetComponent<PowerUpPoolObject>().poolObjectType;
        }

        private void Update()
        {
            this.transform.Rotate(0, 0.5f, 0);
        }

        //public void InvokePowerUp()
        //{
        //    OnInvokePowerUp(this, _powerUpType);
        //    _powerUpManager.UnsubscribeFromPowerUp(this, _powerUpType);
        //}

        public void OnEnable()
        {
            //_powerUpManager.SubscribeToPowerUp(this, _powerUpType);
            //_soundManager.SubscribeToPowerUp(this);
        }

        public void OnDisable()
        {
            //_soundManager.SubscribeToPowerUp(this);
            this.transform.position = _levelSpawner.GetRandomPosition(_levelSpawner.minCoordinates,_levelSpawner.maxCoordinates);
        }
    }
}
