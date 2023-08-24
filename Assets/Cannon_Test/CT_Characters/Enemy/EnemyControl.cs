using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public enum TransitionParameter
    {
        Idle,
        Walking_1,
        Walking_2,
    }

    public class EnemyControl : MonoBehaviour, IPooledHittingObject
    {
        [Header("Health")]
        private int _currentHealth;
        public int maxHealth = 1; //Defaul is 1, if does not set on prefab

        [Header("Animation")]
        public bool ragdollTriggered;
        private Animator _animator;
        private Collider _collider;
        [Inject] DeathAnimationManager _deathAnimationManager;
        private float _deathDelayTime = 1.7f;
        public TransitionParameter walkingType;

        [Header("Spawn")]
        public EnemyType enemyType;
        [Inject] private LevelSpawner _levelSpawner;
        private EnemyPoolObject _enemyPoolobject;

        [Header("Bools")]
        public bool isKilled = false;
        private bool _isFreezed = false;
        public bool isMoving;

        [Inject] LevelLogic _levelLogic;
        [Inject] private SoundManager _soundManager;
        [Inject] private PlayerControl _playerControl;

        private void Awake()
        {
            _enemyPoolobject = GetComponent<EnemyPoolObject>();
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponentInChildren<Collider>();
            _currentHealth = maxHealth + (int)Math.Round(_levelLogic.EnemyBonusHealth);
            _animator.speed = _levelLogic.CurrentGlobalEnemyAnimatorSpeed;
        }

        public void OnFreezeStatusChanges()
        {
            _isFreezed = _levelLogic.IsTimerFreezed;
            _animator.speed = _levelLogic.CurrentGlobalEnemyAnimatorSpeed;
        }

        public void OnGotHit(bool instaKill = false)
        {
            _soundManager.PlayRandomSound(AudioSourceType.HIT, true);

            if (instaKill)
            {
                OnGotKilled();
            }

            _currentHealth -= _playerControl.CannonBallAttackDamage;

            if (_currentHealth <= 0)
            {
                OnGotKilled();
            }
        }

        private void OnGotKilled()
        {
            isKilled = true;
            _collider.enabled = false;
            if (!_isFreezed)
            {
                StartCoroutine(Death());
            }
            else
            {
                StartCoroutine(WaitForUnfreeze());
            }
        }

        IEnumerator Death()
        {
            _animator.runtimeAnimatorController = _deathAnimationManager.GetAnimator();
            yield return new WaitForSeconds(_deathDelayTime);
            this.gameObject.SetActive(false);
            _levelLogic.RemoveEnemyFromCounter();
        }

        IEnumerator WaitForUnfreeze()
        {
            while (_isFreezed)
            {
                yield return null;
            }
            StartCoroutine(Death());
        }

        public void MoveForward(float speed)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private T GetRandomValueFromEnum<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(T)).Length);
            var randomValue = values.GetValue(randomIndex);
            var result = (T)Convert.ChangeType(randomValue, typeof(T));
            return result;
        }

        //Refference to animation states
        public void CacheCharacterControl(Animator animator)
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach (CharacterState c in arr)
            {
                c.enemyControl = this;
            }
        }

        public void OnEnable()
        {
            if (!_levelLogic.IsOnMenu)
            {
                _levelLogic.AddEnemyToCounterAndCheckLoseCondition();
                //_levelLogic.SubscribeToEnemy(this);
                //OnEnemySpawned(this);
                _animator.speed = _levelLogic.CurrentGlobalEnemyAnimatorSpeed + _levelLogic.EnemyBonusSpeed;
            }
        }

        public void OnDisable()
        {
            if (!_levelLogic.IsOnMenu)
            {
                isKilled = false;
                GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
                this.transform.position = _levelSpawner.GetRandomPosition(_levelSpawner.maxCoordinates, _levelSpawner.minCoordinates);
                _currentHealth = maxHealth + (int)Math.Round(_levelLogic.EnemyBonusHealth);
                _collider.enabled = true;
                _enemyPoolobject.ReturnToPool();
                _animator.runtimeAnimatorController = _deathAnimationManager.GetDefaultAnimator();
                walkingType = GetRandomValueFromEnum<TransitionParameter>();
            }
        }
    }
}

