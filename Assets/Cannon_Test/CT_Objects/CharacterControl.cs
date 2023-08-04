using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon_Test
{
    public enum TransitionParameter
    {
        Idle,
        Walking_1,
        Walking_2,
    }

    public class CharacterControl : MonoBehaviour
    {
        public TransitionParameter walkingType;

        public bool isMoving;

        public EnemyType enemyType;

        public void MoveForward(float speed)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void Awake()
        {
            walkingType = GetRandomValueFromEnum<TransitionParameter>();
        }

        private T GetRandomValueFromEnum<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(T)).Length);
            var randomValue = values.GetValue(randomIndex);
            var result = (T)Convert.ChangeType(randomValue, typeof(T));
            return result;

        }

        public void CacheCharacterControl(Animator animator)  //Передает стейтам аниматора CharacterControl референс
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach (CharacterState c in arr)
            {
                c.characterControl = this;
            }
        }
    }
}

