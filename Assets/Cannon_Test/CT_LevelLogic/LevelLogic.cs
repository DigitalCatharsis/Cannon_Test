using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Cannon_Test
{
    public class LevelLogic : MonoBehaviour
    {
        [Header("Animation and PowerUps")]
        [SerializeField] private float _defaultGlobalEnemyAnimatorSpeed;
        [SerializeField] private float _currentGlobalEnemyAnimatorSpeed;
        [SerializeField] private float _currentGlobalFreezeTime;
        [SerializeField] private float _maxFreezeTime;  //По ТЗ нужно 3 секунды, но 4 так хорошо ложились под звук powerUp'а :D.  Спокойно можно поменять на 3
        [SerializeField] private bool _isTimerFreezed;

        [Header("UI")]
        [SerializeField] private int _enemyCount;
        [SerializeField] private GameObject _deathUiGameObject;

        [Header("Bools")]
        public bool IsOnMenu;
        public bool isGameOver = false;
        public bool heaveMachineGun = false;

        [Header("GameOver")]
        [SerializeField] private float _secondsUntillRestart = 8;
        [SerializeField] private int _enemiesTillRestart = 10;
        [SerializeField] private string _mainMenuName = "S_MainMenu";

        [Header("Diffult params")]
        public float increaceDifficultyPeriod = 5;
        [SerializeField] private float _enemyBonusHealth = 0;
        [SerializeField] private float _enemyBonusSpeed = 0;
        [SerializeField] private float _spawnSpeedReduceParapeter = 0;

        [SerializeField] private int _attackDamageBonus = 0;




        public float CurrentGlobalEnemyAnimatorSpeed { get => _currentGlobalEnemyAnimatorSpeed; private set => _currentGlobalEnemyAnimatorSpeed = value; }
        public float DefaultGlobalEnemyAnimatorSpeed { get => _defaultGlobalEnemyAnimatorSpeed; private set => _defaultGlobalEnemyAnimatorSpeed = value; }
        public float CurrentGlobalFreezeTimer { get => _currentGlobalFreezeTime; private set => _currentGlobalFreezeTime = value; }
        public float MaxGlobalFreezeTimer { get => _currentGlobalFreezeTime; private set => _currentGlobalFreezeTime = value; }
        public bool IsTimerFreezed { get => _isTimerFreezed; private set => _isTimerFreezed = false; }
        public int EnemyCount { get => _enemyCount; private set => EnemyCount = value; }
        public float EnemyBonusHealth { get => _enemyBonusHealth; private set => _enemyBonusHealth = value; }
        public float EnemyBonusSpeed { get => _enemyBonusSpeed; private set => _enemyBonusSpeed = value; }
        public float SpawnSpeedReduceParapeter { get => _spawnSpeedReduceParapeter; private set => _spawnSpeedReduceParapeter = value; }
        public int AttackDamageBonus { get => _attackDamageBonus;  private set => _attackDamageBonus = value; }

        public void IncreaseAttackDamageBonus(int amount)
        {
            _attackDamageBonus = amount;
        }

        private void Start()
        {
            StartCoroutine(DifficultIncreaserCorutine());
        }

        IEnumerator DifficultIncreaserCorutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(increaceDifficultyPeriod);
                _enemyBonusHealth += 0.1f;
                _enemyBonusSpeed += 0.1f;
                _spawnSpeedReduceParapeter += 0.04f;
            }
        }

        IEnumerator WaitAndGoToMenu()
        {
            yield return new WaitForSeconds(_secondsUntillRestart);
            UnityEngine.SceneManagement.SceneManager.LoadScene(_mainMenuName);
        }

        public void GameOverAndRestart()
        {
            _deathUiGameObject.SetActive(true);
        }

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
        public void AddEnemyToCounterAndCheckLoseCondition()
        {
            _enemyCount++;
            if(_enemyCount >= _enemiesTillRestart)
            {
                isGameOver = true;
                GameOverAndRestart();
                StartCoroutine(WaitAndGoToMenu());
            }
        }
        public void RemoveEnemyFromCounter()
        {
            _enemyCount--;
        }
    }
}
