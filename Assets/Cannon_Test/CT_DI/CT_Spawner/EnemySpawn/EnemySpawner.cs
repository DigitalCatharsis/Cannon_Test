using System;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class EnemySpawner : MonoBehaviour 
    {
        [Inject] private PoolManager _poolManager;
        [Inject] private PlayerControl _playerControl;

        private float _elapsedTime;

        //courutine
        //uniTask?
        //Красивый таймер в апдейтах, кек

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > 1)
            {
                _elapsedTime = 0;

                var obj = _poolManager.GetObject(GetRandomValueFromEnum<EnemyType>(), GetRandomPosition(), Quaternion.LookRotation(_playerControl.transform.position));
                //Debug.Log("spawning: " + obj.name);
                obj.SetActive(true);
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
