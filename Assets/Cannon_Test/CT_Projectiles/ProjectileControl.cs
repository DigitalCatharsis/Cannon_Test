using System.Collections;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectileControl : MonoBehaviour, IPooledHittingObject
    {
        [Inject] PlayerControl _playerControl;        

        private float lifeTime = 4.0f;

        private void OnCollisionEnter(Collision collider)
        {
            if (collider.transform.root.GetComponent<EnemyPoolObject>())
            {
                collider.transform.root.GetComponent<EnemyControl>().OnGotHit();
                KillItSelf();
            }
            else if (collider.transform.root.GetComponent<PowerUpPoolObject>())
            {
                collider.transform.root.GetComponent<PowerUpControl>().InvokePowerUp();
                collider.transform.root.GetComponent<PowerUpPoolObject>().ReturnToPool();
                KillItSelf();
            }
            else
            {
                KillItSelf();
            }
        }

        private void KillItSelf()
        {
            this.gameObject.transform.root.GetComponent<ProjectilePoolObject>().ReturnToPool();
            this.gameObject.SetActive(false);
        }

        IEnumerator KillItselfOverTime()
        {
            yield return new WaitForSeconds(lifeTime);
            KillItSelf();
        }

        public void OnEnable()
        {
            StartCoroutine(KillItselfOverTime());
            this.gameObject.transform.position = _playerControl._cannonBallSpawnPoint.position;
        }
        public void OnDisable()
        {
            
        }
    }
}