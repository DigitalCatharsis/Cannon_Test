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
        [Inject] LevelLogic _levelLogic;

        [Header("Spawn")]
        public EnemyType enemyType;
        [Inject] private LevelSpawner _levelSpawner;
        private EnemyPoolObject _enemyPoolobject;

        [Header("Bools")]
        public bool isKilled = false;
        private bool _isFreezed = false;
        public bool isMoving;

        [Inject] private PowerUpManager _powerUpManager;

        public delegate void EnemyHandler(EnemyControl enemyControl);
        public event EnemyHandler? OnEnemySpawned;
        public event EnemyHandler? OnEnemyKilled;

        private void Awake()
        {
            _enemyPoolobject = GetComponent<EnemyPoolObject>();
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponentInChildren<Collider>();
            _currentHealth = maxHealth;
            _animator.speed = _levelLogic.CurrentGlobalEnemyAnimatorSpeed;

            SubscribeToPowerManager(_powerUpManager);
        }
        public void SubscribeToPowerManager(PowerUpManager powerUpManager)
        {
            _powerUpManager.NotifyFreeze += OnFreezeStatusChanges;
        }

        private void OnFreezeStatusChanges()
        {
            _isFreezed = _levelLogic.IsTimerFreezed;
            _animator.speed = _levelLogic.CurrentGlobalEnemyAnimatorSpeed;
        }

        public void OnGotHit(bool instaKill = false)
        {
            if (instaKill)
            {
                OnGotKilled();
            }

            _currentHealth -= 1;

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
            //yield return new WaitForEndOfFrame();
            this.gameObject.SetActive(false);
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
            _levelLogic.SubscribeToEnemy(this);
            OnEnemySpawned(this);
            _animator.speed = _levelLogic.CurrentGlobalEnemyAnimatorSpeed;
        }

        public void OnDisable()
        {
            OnEnemyKilled(this);
            isKilled = false;
            GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = _levelSpawner.GetRandomPosition(_levelSpawner.maxCoordinates, _levelSpawner.minCoordinates);
            _currentHealth = maxHealth;
            _collider.enabled = true;
            _enemyPoolobject.ReturnToPool();
            _animator.runtimeAnimatorController = _deathAnimationManager.GetDefaultAnimator();
            walkingType = GetRandomValueFromEnum<TransitionParameter>();
        }
    }
}

