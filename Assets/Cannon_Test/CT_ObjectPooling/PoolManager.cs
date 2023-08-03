using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PoolManager : MonoBehaviour
    {
        private ICoreFactory<EnemyType> factory;

        public Dictionary<EnemyType, List<GameObject>> enemyPoolDictionary = new Dictionary<EnemyType, List<GameObject>>();
        public Dictionary<ProjectileType, List<GameObject>> projectilePoolDictionary = new Dictionary<ProjectileType, List<GameObject>>();

        //Дженерик метод для заполнения словаря. 
        public void SetUpDictionary<T>(T type, Dictionary<T, List<GameObject>> poolDictionary) where T : Enum
        {
            T[] arr = Enum.GetValues(typeof(T)) as T[];

            foreach (T p in arr)
            {
                if (!poolDictionary.ContainsKey(p))
                {
                    poolDictionary.Add(p, new List<GameObject>());
                }
            }
        }

        public GameObject GetObject<T,Z>(T objType, Dictionary<T, List<GameObject>> poolDictionary) where T : Enum   
        {
            if (poolDictionary.Count == 0)
            {
                SetUpDictionary(objType, poolDictionary);
            }

            List<GameObject> list = poolDictionary[objType];
            GameObject obj = null;

            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }

            else
            {
                obj = PoolObjectLoader<ICoreFactory<T>,T>.InstantiatePrefab(GetRandomValueFromEnum<T>(objType) , GetRandomPosition()).gameObject;
            }
            return obj;
        }

        private float GetRandomFloat(float start, float end)
        {
            return UnityEngine.Random.Range(start, end);
        }
        private T GetRandomValueFromEnum<T>(T type) where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(T)).Length);
            var randomValue = values.GetValue(randomIndex);

            return (T)Convert.ChangeType(randomValue, typeof(T));

        }
        private Vector3 GetRandomPosition()
        {
            var position = new Vector3(GetRandomFloat(-22.0f, 22.0f), 0.50f, GetRandomFloat(-5.0f, 10.0f));
            return position;
        }
        public void AddObject<T,Y>(PoolObject obj, Y poolDictionary) where T : Enum where Y : Dictionary<T, List<GameObject>> //Добавляем, когда poolObject выключаетcя.....я хочу спать...
        {
            List<GameObject> list = poolDictionary[(T)obj.poolObjectType];
            list.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }

        private void Update()
        {
            this.GetObject<EnemyType, EnemyFactory>(EnemyType.ZOMBIE_GENERAL, enemyPoolDictionary);
        }
    }
}
