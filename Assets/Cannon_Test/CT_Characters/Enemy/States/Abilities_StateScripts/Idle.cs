using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cannon_Test
{
    [CreateAssetMenu(fileName = "New state", menuName = "Cannon_Test/AbilityData/Idle")]
    public class Idle : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.enemyControl.isMoving)
            {
                animator.SetBool(characterState.enemyControl.walkingType.ToString(), true);
            }
        }
    }
}