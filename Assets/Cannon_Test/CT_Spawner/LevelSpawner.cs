using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class LevelSpawner : MonoBehaviour
    {
        [Inject] private PoolManager _poolManager;
        [Inject] private LevelLogic _levelLogic;

        [Header("SpawnTimer")]
        [SerializeField] private float _enemySpawnTimer;  //Seconds to spawn
        [SerializeField] private float _powerUpSpawnTimer;  //Seconds to spawn

        [Header("SpawnZoneCoordinates")]
        public Vector3 minCoordinates = new Vector3(-10f, 0.50f, -0.9f);
        public Vector3 maxCoordinates = new Vector3(10, 0.50f, 3.81f);

        [SerializeField] private float _minimalEnemySpawnTimer = 0.5f;

        //courutine
        //uniTask?
        //Красивый таймер в апдейтах, кек w

        private void Start()
        {
            StartCoroutine(SpawnEnemyCorutine());
            if (!_levelLogic.IsOnMenu)
            {
                StartCoroutine(SpawnHeavyMachineGun());
                StartCoroutine(SpawnRandomPowerUpCorutine());
            }
        }

        private IEnumerator SpawnHeavyMachineGun()
        {
            yield return new WaitForSeconds(GetRandomFloat(1f,60f));
            var projectileObj = _poolManager.GetObject(PowerUpType.HeavyMachineGun, GetRandomPosition(minCoordinates, maxCoordinates), Quaternion.Euler(0, 90, 0));
            projectileObj.SetActive(true);
        }

        private IEnumerator SpawnEnemyCorutine()
        {
            while (true)
            {
                if (!_levelLogic.IsTimerFreezed)
                {
                    var enemyObj = _poolManager.GetObject(GetRandomValueFromEnum<EnemyType>(), GetRandomPosition(minCoordinates, maxCoordinates), Quaternion.Euler(0, 180, 0));
                    enemyObj.SetActive(true);
                    if (_enemySpawnTimer >= _minimalEnemySpawnTimer)
                    {
                        _enemySpawnTimer -= _levelLogic.SpawnSpeedReduceParapeter;
                    }
                    else if (_enemySpawnTimer <= _minimalEnemySpawnTimer) 
                    {
                        _enemySpawnTimer = _minimalEnemySpawnTimer;
                    }
                }
                yield return new WaitForSeconds(_enemySpawnTimer);
            }
        }

        private IEnumerator SpawnRandomPowerUpCorutine()
        {
            while (true)
            {
                var randomPowerUpType = PowerUpType.HeavyMachineGun;

                while (randomPowerUpType == PowerUpType.HeavyMachineGun)   //Only one machinegun powerup per game
                {
                    randomPowerUpType = GetRandomValueFromEnum<PowerUpType>();
                }
                var projectileObj = _poolManager.GetObject(randomPowerUpType, GetRandomPosition(minCoordinates, maxCoordinates), Quaternion.Euler(0, 90, 0));
                projectileObj.SetActive(true);
                yield return new WaitForSeconds(_powerUpSpawnTimer);
            }

        }
        private T GetRandomValueFromEnum<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(T)).Length);
            var randomValue = values.GetValue(randomIndex);
            var result = (T)Convert.ChangeType(randomValue, typeof(T));
            return result;
        }
        public Vector3 GetRandomPosition(Vector3 minCoordinates, Vector3 maxCoordinates)
        {
            var position = new Vector3(this.GetRandomFloat(minCoordinates.x, maxCoordinates.x), this.GetRandomFloat(minCoordinates.y, maxCoordinates.y), this.GetRandomFloat(minCoordinates.z, maxCoordinates.z));
            return position;
        }
        public float GetRandomFloat(float start, float end)
        {
            return UnityEngine.Random.Range(start, end);
        }
    }
}
