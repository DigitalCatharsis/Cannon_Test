using UnityEngine;
using Zenject;

namespace Cannon_Test
{

    //GameConfigurator
    public class FactoryInstaller : MonoInstaller
    {
        [Header("Player spawn")]
        [SerializeField] private PlayerControl _player;
        [SerializeField] private Transform _playerLocation;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindPlayer();
            BindEnemySpawn();
        }
        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<FactoryInstaller>().FromInstance(this).AsSingle().NonLazy();
        }
        private void BindEnemySpawn()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().NonLazy();
            Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle().NonLazy();
        }

        public void BindPlayer()
        {
            Container.Bind<PlayerControl>().FromComponentInNewPrefab(_player).AsSingle().NonLazy();
        }
    }
}

