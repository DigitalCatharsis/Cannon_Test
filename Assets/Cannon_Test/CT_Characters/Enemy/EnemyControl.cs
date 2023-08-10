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
        public bool isMoving;
        public bool ragdollTriggered;
        private Animator _animator;
        private Collider _collider;
        [Inject] DeathAnimationManager _deathAnimationManager;
        public float deathDelayTime = 3.0f;
        public TransitionParameter walkingType;
        [Inject] LevelLogic _levelLogic;

        [Header ("Spawn")]
        public EnemyType enemyType;
        [Inject] private LevelSpawner _spawner;
        private EnemyPoolObject _enemyPoolobject;

        private void Awake()
        {
            walkingType = GetRandomValueFromEnum<TransitionParameter>();
            _enemyPoolobject = GetComponent<EnemyPoolObject>();
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponentInChildren<Collider>();
            _currentHealth = maxHealth;
        }
        public void OnTurnOff()
        {
            GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = _spawner.GetRandomPosition();
            _currentHealth = maxHealth;
        }

        public void OnGotHit(bool instaKill = false)
        {
            if (instaKill)
            {
                _currentHealth = 0;
            }
            else
            {
                _currentHealth -= 1;
            }

            if (_currentHealth <= 0)
            {
                _collider.enabled = false;
                StartCoroutine(Death(deathDelayTime));
            }
        }

        

        IEnumerator Death(float delayTime)
        {
            _animator.runtimeAnimatorController = _deathAnimationManager.GetAnimator();
            yield return new WaitForSeconds(delayTime);
            _collider.enabled = true;
            _enemyPoolobject.GotKilled();
            _animator.runtimeAnimatorController = _deathAnimationManager.GetDefaultAnimator();
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

        public void CacheCharacterControl(Animator animator)  
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach (CharacterState c in arr)
            {
                c.enemyControl = this;
            }
        }

        private void OnEnable()
        {
            _animator.speed = _levelLogic.GlobalEnemyAnimatorSpeed;
        }
    }
}

