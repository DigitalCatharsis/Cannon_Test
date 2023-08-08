using System;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class LevelSpawner : MonoBehaviour 
    {
        [Inject] private PoolManager _poolManager;
        [Inject] private PlayerControl _playerControl;

        private float _elapsedTime;
        private float _timer;  //Seconds to spawn

        private void Awake()
        {
            _timer = 3;
        }

        //courutine
        //uniTask?
        //Красивый таймер в апдейтах, кек w

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _timer)
            {
                _elapsedTime = 0;

                var enemyObj = _poolManager.GetObject(GetRandomValueFromEnum<EnemyType>(), GetRandomPosition(), Quaternion.Euler(0, 180, 0));
                var projectileObj = _poolManager.GetObject(GetRandomValueFromEnum<PowerUpType>(), GetRandomPosition(), Quaternion.Euler(0,90,0));
                //Debug.Log("spawning: " + obj.name);
                enemyObj.SetActive(true);
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
