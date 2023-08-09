using UnityEngine;

namespace Cannon_Test
{
    public class ProjectileControl : MonoBehaviour
    {
        private float lifeTime = 2.0f;
        private void OnCollisionEnter(Collision collider)
        {
            Debug.Log(collider.gameObject.name);

            if (collider.transform.root.GetComponent<EnemyPoolObject>())
            {
                collider.transform.root.GetComponent<EnemyPoolObject>().GotKilled();
                Destroy(this.gameObject);
            }

            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.root.GetComponent<PowerUpPoolObject>())
            {
                collider.transform.root.GetComponent<PowerUpPoolObject>().GotKilled();
            }
        }

        private void Awake()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}