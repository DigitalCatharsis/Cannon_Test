using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PoolObjectLoader
    {
        [Inject]
        private ICoreFactory<EnemyType> _factory;

        public GameObject InstantiatePrefab(EnemyType poolObjectType, Vector3 position, Quaternion rotation)
        {
            return _factory.AddToPool(poolObjectType, position, rotation);
        }
    }

}

