using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Cannon_Test
{
    public class PoolManager : MonoBehaviour
    {
        public Dictionary<EnemyType, List<GameObject>> enemyPoolDictionary = new Dictionary<EnemyType, List<GameObject>>();
        public Dictionary<PowerUpType, List<GameObject>> powerUpPoolDictionary = new Dictionary<PowerUpType, List<GameObject>>();

        [Inject] private PoolObjectLoader _poolObjectLoader;

        #region SetupDictionary
        private void SetUpDictionary<T>(T objType)
        {
            object dictionary = objType switch
            {
                EnemyType enemyType => DictionarySetuper(enemyType, enemyPoolDictionary),
                PowerUpType powerUpType => DictionarySetuper(powerUpType, powerUpPoolDictionary),
                var unknownType => throw new Exception($"{unknownType?.GetType()}")
            };
        }
        private Dictionary<T, List<GameObject>> DictionarySetuper<T>(T objType, Dictionary<T, List<GameObject>> dictionary)
        {
            T[] arr = Enum.GetValues(typeof(T)) as T[];

            foreach (T p in arr)
            {
                if (!dictionary.ContainsKey(p))
                {
                    dictionary.Add(p, new List<GameObject>());
                }
            }
            return dictionary;
        }
        #endregion

        #region GetObjectFromPool
        public GameObject GetObject<T>(T objType, Vector3 position, Quaternion rotation)
        {
            var typeList = objType switch
            {
                EnemyType enemyType => ObjectGetter(enemyPoolDictionary, enemyType, position, rotation),
                PowerUpType powerUpType => ObjectGetter(powerUpPoolDictionary, powerUpType, position, rotation),
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
                default:
                    throw new Exception($"{objType?.GetType()}");
            }
        }
        //private void ObjectAdder<T>(List<GameObject> list, T objType)
        //{
        //    list.Add(objType.gameObject);
        //    objType.gameObject.SetActive(false);
        //}

        //public void AddEnemyObject(EnemyPoolObject obj)
        //{
        //    List<GameObject> list = enemyPoolDictionary[(EnemyType)obj.poolObjectType];
        //    list.Add(obj.gameObject);
        //    obj.gameObject.SetActive(false);
        //}

        //public void AddProjectileObject(PowerUpPoolObject obj)
        //{
        //    List<GameObject> list = powerUpPoolDictionary[(PowerUpType)obj.poolObjectType];
        //    list.Add(obj.gameObject);
        //    obj.gameObject.SetActive(false);
        //}

        //public void SetUpEnemyDictionary()
        //{
        //    EnemyType[] arr = Enum.GetValues(typeof(EnemyType)) as EnemyType[];

        //    foreach (EnemyType p in arr)
        //    {
        //        if (!enemyPoolDictionary.ContainsKey(p))
        //        {
        //            enemyPoolDictionary.Add(p, new List<GameObject>());
        //        }
        //    }
        //}
        //public void SetUpProjectileDictionary()
        //{
        //    PowerUpType[] arr = Enum.GetValues(typeof(PowerUpType)) as PowerUpType[];

        //    foreach (PowerUpType p in arr)
        //    {
        //        if (!powerUpPoolDictionary.ContainsKey(p))
        //        {
        //            powerUpPoolDictionary.Add(p, new List<GameObject>());
        //        }
        //    }
        //}

        //public GameObject GetEnemyObject(EnemyType objType, Vector3 position, Quaternion rotation)
        //{
        //    if (enemyPoolDictionary.Count == 0)
        //    {
        //        SetUpEnemyDictionary();
        //    }

        //    List<GameObject> list = enemyPoolDictionary[objType];
        //    GameObject obj = null;

        //    if (list.Count > 0)
        //    {
        //        obj = list[0];
        //        list.RemoveAt(0);
        //    }

        //    else
        //    {
        //        obj = _poolObjectLoader.InstantiateEnemyPrefab(objType, position, rotation);
        //    }
        //    return obj;
        //}

        //public GameObject GetProjectileObject(PowerUpType objType, Vector3 position, Quaternion rotation)
        //{
        //    if (powerUpDictionary.Count == 0)
        //    {
        //        SetUpProjectileDictionary();
        //    }

        //    List<GameObject> list = powerUpDictionary[objType];
        //    GameObject obj = null;

        //    if (list.Count > 0)
        //    {
        //        obj = list[0];
        //        list.RemoveAt(0);
        //    }

        //    else
        //    {
        //        obj = _poolObjectLoader.InstantiateProjectilePrefab(objType, position, rotation);
        //    }
        //    return obj;
        //}
    }
}
