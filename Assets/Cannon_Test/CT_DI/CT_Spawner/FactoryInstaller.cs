using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Cannon_Test
{
    public class FactoryInstaller : MonoInstaller, IInitializable
    {
        [SerializeField][NonReorderable] private EnemySpawnMarker[] _enemyMarkers;   //[NonReorderable] убрать баг UI
        [SerializeField] private GameObject _player;
        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindEnemyFactory();
        }
        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<FactoryInstaller>().FromInstance(this).AsSingle().NonLazy();
            GetRandomPosition();
        }
        private void BindEnemyFactory()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().NonLazy();
        }
        //private void BindPlayer()
        //{
        //    var foo = Container.InstantiatePrefabResourceForComponent<GameObject>("Cannon", new object[] { "Cannon", 6.0f });

        //    Container.Bind<CharacterControl>().FromInstance(playerInstance).AsSingle().NonLazy();
        //}
        public void Initialize()
        {
            var enemyFactory = Container.Resolve<IEnemyFactory>();
            enemyFactory.Load();

            foreach (var marker in _enemyMarkers)
            {
                enemyFactory.Create(marker.enemyType, GetRandomPosition());//marker.transform.position);
            }
        }

        private Vector3 GetRandomPosition()
        {
            var position = new Vector3(Random.Range(-50.0f, 50.0f), 50, Random.Range(-5.0f, 10.0f));
            return position;
        }
    }
}
