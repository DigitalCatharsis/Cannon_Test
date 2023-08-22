using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectileFactory : ICoreFactory<ProjectileType> //IEnemyFactory
    {

        [Inject] private DiContainer _diContainer;

        private readonly Object _JackCannonBallPrefab;
        private readonly Object _JackFaceCannonBallPrefab;

        private readonly string _JackCannonBall;
        private readonly string _JackFaceCannonBall;

        public ProjectileFactory()
        {
            _JackCannonBall = ProjectileType.JackCannonball.ToString();
            _JackFaceCannonBall = ProjectileType.JackFaceCannonball.ToString();

            _JackCannonBallPrefab = Resources.Load(_JackCannonBall) as GameObject;
            _JackFaceCannonBallPrefab = Resources.Load(_JackFaceCannonBall) as GameObject;
        }

        public GameObject AddToPool(ProjectileType enemyType, Vector3 position, Quaternion rotation)
        {
            switch (enemyType)
            {
                case ProjectileType.JackCannonball:
                    {
                        return _diContainer.InstantiatePrefab(_JackCannonBallPrefab, position, rotation, null);
                    }
                case ProjectileType.JackFaceCannonball:
                    {

                        return _diContainer.InstantiatePrefab(_JackFaceCannonBallPrefab, position, rotation, null);
                    }
                default:  //Интересно, как заткнуть эту дыру грамотно....
                    {
                        return null;
                    }
            }
        }
    }
}
