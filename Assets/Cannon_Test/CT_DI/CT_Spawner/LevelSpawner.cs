using System;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class LevelSpawner : MonoBehaviour
    {
        [Inject] private PoolManager _poolManager;
        [Inject] private LevelLogic _levelLogic;

        private float _enemyElapsedTime;
        private float _powerUpElapsedTime;
        [SerializeField] private float _enemySpawnTimer;  //Seconds to spawn
        [SerializeField] private float _powerUpSpawnTimer;  //Seconds to spawn

        private void Awake()
        {
            _enemySpawnTimer = GetRandomFloat(3.0f, 5.0f);
            _powerUpSpawnTimer = GetRandomFloat(10.0f, 15.0f);
        }

        //courutine
        //uniTask?
        //Красивый таймер в апдейтах, кек w

        private void Update()
        {
            if (!_levelLogic.IsTimerFreezed)
            {
                SpawnEnemy();
            }            
            SpawnPowerUp();
        }

        private void SpawnEnemy()
        {
            _enemyElapsedTime += Time.deltaTime;

            if (_enemyElapsedTime > _enemySpawnTimer)
            {
                _enemyElapsedTime = 0;

                var enemyObj = _poolManager.GetObject(GetRandomValueFromEnum<EnemyType>(), GetRandomPosition(), Quaternion.Euler(0, 180, 0));
                //Debug.Log("spawning: " + obj.name);
                enemyObj.SetActive(true);
            }
        }
        private void SpawnPowerUp()
        {
            _powerUpElapsedTime += Time.deltaTime;

            if (_powerUpElapsedTime > _powerUpSpawnTimer)
            {
                _powerUpElapsedTime = 0;

                var projectileObj = _poolManager.GetObject(GetRandomValueFromEnum<PowerUpType>(), GetRandomPosition(), Quaternion.Euler(0, 90, 0));
                projectileObj.SetActive(true);
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
        public Vector3 GetRandomPosition()
        {
            var position = new Vector3(this.GetRandomFloat(-10.0f, 10.0f), 0.50f, GetRandomFloat(-0.9f, 3.8f));
            return position;
        }
        public float GetRandomFloat(float start, float end)
        {
            return UnityEngine.Random.Range(start, end);
        }
    }
}
