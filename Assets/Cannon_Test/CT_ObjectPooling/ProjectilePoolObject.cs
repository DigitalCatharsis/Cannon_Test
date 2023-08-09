using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectilePoolObject : MonoBehaviour, IPoolObject
    {
        [Inject] private PoolManager _poolManager;

        private ProjectileControl _projectileControl;

        public ProjectileType poolObjectType;

        private void Awake()
        {
            _projectileControl = this.gameObject.GetComponent<ProjectileControl>();
        }

        public void GotKilled()
        {
            if (!_poolManager.projectilePoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
        public void TurnOff()
        {
            _projectileControl.OnTurnOff();
            _poolManager.AddObject(this);
        }

    }
}
