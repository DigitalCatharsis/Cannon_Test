using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectileControl : MonoBehaviour, IPooledHittingObject
    {
        [Inject] private LevelSpawner _spawner;

        private float lifeTime = 2.0f;

        private void OnCollisionEnter(Collision collider)
        {
            //Debug.Log(collider.gameObject.name);

            if (collider.transform.root.GetComponent<EnemyPoolObject>())
            {
                collider.transform.root.GetComponent<EnemyControl>().OnGotHit();
                Destroy(this.gameObject);
            }

            if (collider.transform.root.GetComponent<PowerUpPoolObject>())
            {
                collider.transform.root.GetComponent<PowerUpPoolObject>().GotKilled();
            }

            Destroy(this.gameObject);
        }

        //private void OnTriggerEnter(Collider collider)
        //{
        //    if (collider.transform.root.GetComponent<PowerUpPoolObject>())
        //    {
        //        collider.transform.root.GetComponent<PowerUpPoolObject>().GotKilled();
        //    }
        //}

        private void Awake()
        {
            Destroy(gameObject, lifeTime);
        }
        public void OnTurnOff()
        {
            this.transform.position = _spawner.GetRandomPosition();
        }
    }
}