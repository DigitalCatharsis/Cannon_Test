using UnityEngine;

namespace Cannon_Test
{
    public class LevelLogic: MonoBehaviour
    {
        //Dont forget to set at start
        [SerializeField] private float _defaultGlobalEnemyAnimatorSpeed;
        [SerializeField] private float _currentGlobalEnemyAnimatorSpeed;
        [SerializeField] private float _currentGlobalFreezeTime;
        [SerializeField] private float _maxFreezeTime;
        [SerializeField] private bool _isTimerFreezed;
        
        public float CurrentGlobalEnemyAnimatorSpeed { get => _currentGlobalEnemyAnimatorSpeed; private set => _currentGlobalEnemyAnimatorSpeed = value; }
        public float DefaultGlobalEnemyAnimatorSpeed { get => _defaultGlobalEnemyAnimatorSpeed; private set => _defaultGlobalEnemyAnimatorSpeed = value; }
        public float CurrentGlobalFreezeTimer { get => _currentGlobalFreezeTime; private set => _currentGlobalFreezeTime = value; }
        public float MaxGlobalFreezeTimer { get => _currentGlobalFreezeTime; private set => _currentGlobalFreezeTime = value; }
        public bool IsTimerFreezed { get => _isTimerFreezed; private set => _isTimerFreezed = false; }

        public void StartFreezeTimer()
        {
            _isTimerFreezed = true;
            _currentGlobalFreezeTime = _maxFreezeTime;
        }   
        public void ResetFreezeTimer()
        {
            _isTimerFreezed = false;
            _currentGlobalFreezeTime = 0.0f;
        }        
        public void ResetGlobalAnimatorSpeed()
        {
            _currentGlobalEnemyAnimatorSpeed = _defaultGlobalEnemyAnimatorSpeed;
        }    
        public void FreezeGlobalAnimatorSpeed()
        {
            _currentGlobalEnemyAnimatorSpeed = 0.0f;
        }
    }

}
