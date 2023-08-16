using Newtonsoft.Json.Bson;
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

        [Header("Animation")]
        //[SerializeField] private DeathAnimationManager _deathAnimationManager;
        [SerializeField] private DeathAnimationLoader _deathAnimationLoader;

        [Header("Managers")]
        [SerializeField] private PowerUpManager _powerUpManager;

        [Header("Sound)")]
        [SerializeField] private ShootingSounds _shootingSounds;
        [SerializeField] private SoundManager _soundManager;

        [SerializeField] private LevelLogic _levelLogic;
        [SerializeField] private LevelSpawner _levelSpawner;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindPlayer();
            BindSpawn();
            BindDeathAnimation();
            BindLevelLogic();
            BindManagers();
            BindSounds();
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
            Container.Bind<PoolObjectLoader>().AsSingle().NonLazy();

            Container.Bind<ICoreFactory<EnemyType>>().To<EnemyFactory>().AsSingle().NonLazy();
            Container.Bind<ICoreFactory<PowerUpType>>().To<PowerUpFactory>().AsSingle().NonLazy();
            Container.Bind<ICoreFactory<ProjectileType>>().To<ProjectileFactory>().AsSingle().NonLazy();

            Container.Bind<LevelSpawner>().FromComponentInHierarchy(_levelSpawner).AsSingle().NonLazy();
        }

        private void BindDeathAnimation()
        {
            Container.Bind<DeathAnimationLoader>().FromComponentInNewPrefab(_deathAnimationLoader).AsSingle().NonLazy();
        }
        private void BindLevelLogic()
        {
            Container.Bind<LevelLogic>().FromComponentInHierarchy(_levelLogic).AsSingle().NonLazy();
        }

        private void BindManagers()
        {
            Container.Bind<PoolManager>().AsSingle().NonLazy();
            Container.Bind<DeathAnimationManager>().AsSingle().NonLazy();
            Container.Bind<PowerUpManager>().FromComponentInNewPrefab(_powerUpManager).AsSingle().NonLazy();
        }

        private void BindSounds()
        {
            Container.Bind<ShootingSounds>().FromNewScriptableObject(_shootingSounds).AsSingle().NonLazy();
            Container.Bind<SoundManager>().FromComponentInNewPrefab(_soundManager).AsSingle().NonLazy();
        }
    }
}

