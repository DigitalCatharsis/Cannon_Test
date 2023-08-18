using System.Collections;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{ public class PowerUpManager : MonoBehaviour
    {
        [Inject] private LevelLogic _levelLogic;
        [Inject] private SoundManager _soundManager;

        //public delegate void FreezeHandler();
        //public event FreezeHandler? NotifyFreeze;

        //public void Invoke_FreezeStatusChanged()
        //{
        //    NotifyFreeze.Invoke();
        //}

        //public void SubscribeToPowerUp(PowerUpControl powerUp, PowerUpType type)
        //{
        //    powerUp.OnInvokePowerUp += ExecutePowerUp;
        //}
        //public void UnsubscribeFromPowerUp(PowerUpControl powerUp, PowerUpType type)
        //{
        //    powerUp.OnInvokePowerUp -= ExecutePowerUp;
        //}

        public void ExecutePowerUp(PowerUpControl powerUp)
        {
            _soundManager.PlayPowerUpSound(powerUp);
            EnemyControl[] enemiesControlls = FindObjectsOfType<EnemyControl>();

            switch (powerUp._powerUpType)
            {
                case PowerUpType.FreezeTimer:
                    {
                        StopAllCoroutines();
                        _levelLogic.StartFreezeTimer();
                        StartCoroutine(FreezeGlobalAnimation(enemiesControlls));
                        foreach (var enemy in enemiesControlls)
                        {
                            enemy.OnFreezeStatusChanges();
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

        public IEnumerator FreezeGlobalAnimation(EnemyControl[] enemiesControlls)
        {
            _levelLogic.FreezeGlobalAnimatorSpeed();  //We set it to zero to make sure that turned on and new object had the same speed
            _levelLogic.StartFreezeTimer();
            yield return new WaitForSeconds(_levelLogic.CurrentGlobalFreezeTimer);
            _levelLogic.ResetGlobalAnimatorSpeed();
            _levelLogic.ResetFreezeTimer();

            foreach (var elem in enemiesControlls)
            {
                elem.OnFreezeStatusChanges();
            }
        }
    }
}