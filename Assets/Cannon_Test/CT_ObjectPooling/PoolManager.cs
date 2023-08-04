using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PoolManager : MonoBehaviour
    {
        public Dictionary<EnemyType, List<GameObject>> enemyPoolDictionary = new Dictionary<EnemyType, List<GameObject>>();
        public Dictionary<ProjectileType, List<GameObject>> projectilePoolDictionary = new Dictionary<ProjectileType, List<GameObject>>();

        [Inject] private PoolObjectLoader _poolObjectLoader;

        //Дженерик метод для заполнения словаря. 
        public void SetUpDictionary()
        {
            EnemyType[] arr = Enum.GetValues(typeof(EnemyType)) as EnemyType[];

            foreach (EnemyType p in arr)
            {
                if (!enemyPoolDictionary.ContainsKey(p))
                {
                    enemyPoolDictionary.Add(p, new List<GameObject>());
                }
            }
        }

        public GameObject GetObject(EnemyType objType, Vector3 position, Quaternion rotation)
        {
            if (enemyPoolDictionary.Count == 0)
            {
                SetUpDictionary();
            }

            List<GameObject> list = enemyPoolDictionary[objType];
            GameObject obj = null;

            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }

            else
            {
                obj = _poolObjectLoader.InstantiatePrefab(objType, position, rotation);
            }
            return obj;
        }

        public void AddObject(PoolObject obj) 
        {
            List<GameObject> list = enemyPoolDictionary[(EnemyType)obj.poolObjectType];
            list.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }
    }
}
