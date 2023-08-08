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
            BindSpawn();
        }
        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<GameInstaller>().FromInstance(this).AsSingle().NonLazy();
        }

        public void BindPlayer()
        {
            Container.Bind<PlayerControl>().FromComponentInNewPrefab(_playerControl).AsSingle().NonLazy();
        }
        private void BindSpawn()
        {
            Container.Bind<PoolManager>().FromComponentInNewPrefab(_poolManager).AsSingle().NonLazy();
            Container.Bind<PoolObjectLoader>().AsSingle().NonLazy();

            Container.Bind<ICoreFactory<EnemyType>>().To<EnemyFactory>().AsSingle().NonLazy();
            Container.Bind<ICoreFactory<PowerUpType>>().To<PowerUpFactory>().AsSingle().NonLazy();
            Container.Bind<ICoreFactory<ProjectileType>>().To<ProjectileFactory>().AsSingle().NonLazy();

            Container.Bind<LevelSpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}

