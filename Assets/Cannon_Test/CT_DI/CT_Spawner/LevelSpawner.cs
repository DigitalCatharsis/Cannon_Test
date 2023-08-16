using System;
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
        private float _enemyElapsedTime;
        private float _powerUpElapsedTime;

        [Header("SpawnZoneCoordinates")]
        public Vector3 minCoordinates = new Vector3(-10f, 0.50f, 5.31f);
        public Vector3 maxCoordinates = new Vector3(7.14f, 0.50f, 32.0f);

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

                var enemyObj = _poolManager.GetObject(GetRandomValueFromEnum<EnemyType>(), GetRandomPosition(minCoordinates, maxCoordinates), Quaternion.Euler(0, 180, 0));                
                enemyObj.SetActive(true);                
            }
        }
        private void SpawnPowerUp()
        {
            _powerUpElapsedTime += Time.deltaTime;

            if (_powerUpElapsedTime > _powerUpSpawnTimer)
            {
                _powerUpElapsedTime = 0;

                var projectileObj = _poolManager.GetObject(GetRandomValueFromEnum<PowerUpType>(), GetRandomPosition(minCoordinates,maxCoordinates), Quaternion.Euler(0, 90, 0));
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
