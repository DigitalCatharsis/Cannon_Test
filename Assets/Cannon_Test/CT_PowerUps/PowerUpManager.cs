using System.Collections;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{ public class PowerUpManager : MonoBehaviour
    {
        [Inject] private LevelLogic _levelLogic;

        public delegate void FreezeHandler();
        public event FreezeHandler? NotifyFreeze;

        public void Invoke_FreezeStatusChanged()
        {
            NotifyFreeze.Invoke();
        }

        public void SubscribeToPowerUp(PowerUpControl powerUp)
        {
            powerUp.OnInvokePowerUp += ExecutePowerUp;
        }
        public void UnsubscribeFromPowerUp(PowerUpControl powerUp)
        {
            powerUp.OnInvokePowerUp -= ExecutePowerUp;
        }

        public void ExecutePowerUp(PowerUpControl powerUp, PowerUpType _powerUpType)
        {
            EnemyControl[] enemiesControlls = FindObjectsOfType<EnemyControl>();

            switch (_powerUpType)
            {
                case PowerUpType.FreezeTimer:
                    {
                        _levelLogic.StartFreezeTimer();
                        foreach (var enemy in enemiesControlls)
                        {
                            Animator anim = enemy.transform.root.GetComponentInChildren<Animator>();
                            StartCoroutine(FreezeAnimation(anim));
                        }
                        break;
                    }
                case PowerUpType.Blow:
                    {
                        foreach (var enemy in enemiesControlls)
                        {
                            if (!enemy.isKilled)
                            {
                                enemy.OnGotHit(instaKill: true);
                            }
                        }
                        break;
                    }
            }            
        }

        public IEnumerator FreezeAnimation(Animator anim)
        {
            _levelLogic.FreezeGlobalAnimatorSpeed();  //We set it to zero to make sure that turned on and new object had the same speed
            Invoke_FreezeStatusChanged();
            yield return new WaitForSeconds(_levelLogic.CurrentGlobalFreezeTimer);
            _levelLogic.ResetGlobalAnimatorSpeed();
            _levelLogic.ResetFreezeTimer();
            Invoke_FreezeStatusChanged();
        }
    }
}