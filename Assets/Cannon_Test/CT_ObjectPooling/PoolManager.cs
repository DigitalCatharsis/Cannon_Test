using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PoolManager
    {
        public Dictionary<EnemyType, List<GameObject>> enemyPoolDictionary = new Dictionary<EnemyType, List<GameObject>>();
        public Dictionary<PowerUpType, List<GameObject>> powerUpPoolDictionary = new Dictionary<PowerUpType, List<GameObject>>();
        public Dictionary<ProjectileType, List<GameObject>> projectilePoolDictionary = new Dictionary<ProjectileType, List<GameObject>>();

        [Inject] private PoolObjectLoader _poolObjectLoader;

        #region SetupDictionary

        private void SetUpDictionary<T>(T objType)
        {
            switch (objType)
            {
                case EnemyType enemyType:
                    DictionarySetuper(enemyType, enemyPoolDictionary);
                    break;
                case PowerUpType powerUpType:
                    DictionarySetuper(powerUpType, powerUpPoolDictionary);
                    break;
                case ProjectileType projectileType:
                    DictionarySetuper(projectileType, projectilePoolDictionary);
                    break;
                default:
                    throw new Exception($"{objType?.GetType()}");
            }
        }

        private void DictionarySetuper<T>(T objType, Dictionary<T, List<GameObject>> dictionary)
        {
            T[] arr = Enum.GetValues(typeof(T)) as T[];

            foreach (T p in arr)
            {
                if (!dictionary.ContainsKey(p))
                {
                    dictionary.Add(p, new List<GameObject>());
                }
            }
        }
        #endregion

        #region GetObjectFromPool
        public GameObject GetObject<T>(T objType, Vector3 position, Quaternion rotation)
        {
            var typeList = objType switch
            {
                EnemyType enemyType => ObjectGetter(enemyPoolDictionary, enemyType, position, rotation),
                PowerUpType powerUpType => ObjectGetter(powerUpPoolDictionary, powerUpType, position, rotation),
                ProjectileType projectileType => ObjectGetter(projectilePoolDictionary, projectileType, position, rotation),
                var unknownType => throw new Exception($"{unknownType?.GetType()}")
            };
            return typeList;
        }
        private GameObject ObjectGetter<T>(Dictionary<T, List<GameObject>> pool, T objType, Vector3 position, Quaternion rotation)
        {
            if (pool.Count == 0)
            {
                SetUpDictionary(objType);
            }

            List<GameObject> list = pool[objType];
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
        #endregion

        public void AddObject<T>(T objType)
        {
            List<GameObject> list;

            switch (objType)
            {
                case EnemyPoolObject enemyType:
                    list = enemyPoolDictionary[(EnemyType)enemyType.poolObjectType];
                    list.Add(enemyType.gameObject);
                    enemyType.gameObject.SetActive(false);
                    break;
                case PowerUpPoolObject powerUpType:
                    list = powerUpPoolDictionary[(PowerUpType)powerUpType.poolObjectType];
                    list.Add(powerUpType.gameObject);
                    powerUpType.gameObject.SetActive(false);
                    break;
                case ProjectilePoolObject projectileType:
                    list = projectilePoolDictionary[(ProjectileType)projectileType.poolObjectType];
                    list.Add(projectileType.gameObject);
                    projectileType.gameObject.SetActive(false);
                    break;
                default:
                    throw new Exception($"{objType?.GetType()}");
            }
        }
    }
}
