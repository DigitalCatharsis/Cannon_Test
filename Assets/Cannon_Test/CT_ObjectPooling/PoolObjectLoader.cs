using System;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class PoolObjectLoader
    {

        [Inject] private ICoreFactory<EnemyType> _enemyFactory;
        [Inject] private ICoreFactory<PowerUpType> _powerUpFactory;
        [Inject] private ICoreFactory<ProjectileType> _projectileFactory;

        public GameObject InstantiatePrefab<T>(T objType, Vector3 position, Quaternion rotation)
        {
            var typelist = objType switch
            {
                EnemyType enemyType => InstantiateEnemyPrefab(enemyType, position, rotation),
                PowerUpType powerUpType => InstantiatePowerUpPrefab(powerUpType, position, rotation),
                ProjectileType projectileType => InstantiateProjectilePrefab(projectileType, position, rotation),
                var unknownType => throw new Exception($"{unknownType?.GetType()}")
            };
            return typelist;
        }

        public GameObject InstantiateEnemyPrefab(EnemyType poolObjectType, Vector3 position, Quaternion rotation)
        {
            return _enemyFactory.AddToPool(poolObjectType, position, rotation);
        }

        public GameObject InstantiatePowerUpPrefab(PowerUpType poolObjectType, Vector3 position, Quaternion rotation)
        {
            return _powerUpFactory.AddToPool(poolObjectType, position, rotation);
        }
        public GameObject InstantiateProjectilePrefab(ProjectileType poolObjectType, Vector3 position, Quaternion rotation)
        {
            return _projectileFactory.AddToPool(poolObjectType, position, rotation);
        }
    }
}