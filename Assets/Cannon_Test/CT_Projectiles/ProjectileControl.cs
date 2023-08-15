using System.Collections;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectileControl : MonoBehaviour, IPooledHittingObject
    {
        [Inject] PlayerControl _playerControl;        

        private float lifeTime = 2.0f;

        private void OnCollisionEnter(Collision collider)
        {
            StopCoroutine(KillItselfOverTime());

            if (collider.transform.root.GetComponent<EnemyPoolObject>())
            {
                collider.transform.root.GetComponent<EnemyControl>().OnGotHit();
                KillItSelf();
            }

            if (collider.transform.root.GetComponent<PowerUpPoolObject>())
            {
                collider.transform.root.GetComponent<PowerUpControl>().InvokePowerUp();
                collider.transform.root.GetComponent<PowerUpPoolObject>().ReturnToPool();
            }

            KillItSelf();
        }

        private void Awake()
        {
            Debug.Log("Spawned");
            //StartCoroutine(KillItselfOverTime());
        }

        private void KillItSelf()
        {
            Debug.Log("Killing Itself");
            this.gameObject.transform.root.GetComponent<ProjectilePoolObject>().ReturnToPool();
            Debug.Log("Added myself to PoolManager");
            this.gameObject.SetActive(false);
        }

        IEnumerator KillItselfOverTime()
        {
            yield return new WaitForSeconds(lifeTime);
            KillItSelf();
        }

        public void OnEnable()
        {
            
        }
        public void OnDisable()
        {
            this.gameObject.transform.position = _playerControl._cannonBallSpawnPoint.position;
        }
    }
}