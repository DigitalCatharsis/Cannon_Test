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

