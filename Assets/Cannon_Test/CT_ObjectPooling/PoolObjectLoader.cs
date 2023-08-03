using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PoolObjectLoader<T, Y> where T : ICoreFactory<Y> where Y : System.Enum
    {
        //public List<GameObject> pooledObjects = new List<GameObject>();
        //[SerializeField] private int amountToPool;

        [Inject]
        private static ICoreFactory<Y> _factory;
        public static GameObject InstantiatePrefab(Y poolObjectType, Vector3 position)
        {
            return _factory.AddToPool(poolObjectType, position);
        }
    }

}

