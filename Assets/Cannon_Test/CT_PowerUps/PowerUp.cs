using Cannon_Test;
using System.Collections;
using UnityEngine;
using Zenject;

public class PowerUp : MonoBehaviour, IPooledHittingObject
{
    private PowerUpType _powerUpType;
    [Inject] private LevelSpawner _spawner;
    [Inject] private LevelLogic _levelLogic;

    private void Awake()
    {
        _powerUpType = GetComponent<PowerUpPoolObject>().poolObjectType;
    }

    private void Update()
    {
        this.transform.Rotate(0, 0.5f, 0);
    }
    public void OnTurnOff()
    {
        EnemyControl[] enemiesControlls = FindObjectsOfType<EnemyControl>();

        switch (_powerUpType)
        {
            case PowerUpType.FreezeTimer:
                {
                    foreach(var enemy in  enemiesControlls)
                    {
                        Animator anim = enemy.transform.root.GetComponentInChildren<Animator>();
                        StartCoroutine(FreezeAnimation(enemy.deathDelayTime, anim)); 
                    }
                    break;
                }
            case PowerUpType.Blow:
                {
                    foreach (var enemy in enemiesControlls)
                    {
                        enemy.OnGotHit(instaKill: true);
                    }
                    break;
                }
          }
        this.transform.position = _spawner.GetRandomPosition();
    }

    IEnumerator FreezeAnimation(float delayTime, Animator anim)
    {
        anim.speed = 0f;
        _levelLogic.GlobalEnemyAnimatorSpeed = 0f;
        yield return new WaitForSeconds(delayTime);
        _levelLogic.GlobalEnemyAnimatorSpeed = 1f;
        anim.speed = _levelLogic.GlobalEnemyAnimatorSpeed;
    }



    //private void OnMouseDown()
    //{
    //    (GetComponent<PowerUpPoolObject>()).GotKilled();
    //}

}
