using System;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    //GameConfigurator
    public class GameInstaller : MonoInstaller
    {
        [Header("Player spawn")]
        [SerializeField] private PlayerControl _playerControl;
        [SerializeField] private Transform _playerLocation;

        [Header("Object Pooling")]
        [SerializeField] private PoolManager _poolManager;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindPlayer();
            BindEnemySpawn();
            BindPoolingSystem();
        }

        private void BindPoolingSystem()
        {
            Container.Bind<PoolObjectLoader>().AsSingle().NonLazy();
            Container.Bind<PoolManager>().FromComponentInNewPrefab(_poolManager).AsSingle().NonLazy();
        }

        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<GameInstaller>().FromInstance(this).AsSingle().NonLazy();
        }

        private void BindEnemySpawn()
        {
            Container.Bind<ICoreFactory<EnemyType>>().To<EnemyFactory>().AsSingle().NonLazy();
            Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle().NonLazy();            
        }

        public void BindPlayer()
        {
            Container.Bind<PlayerControl>().FromComponentInNewPrefab(_playerControl).AsSingle().NonLazy();
        }
    }
}

