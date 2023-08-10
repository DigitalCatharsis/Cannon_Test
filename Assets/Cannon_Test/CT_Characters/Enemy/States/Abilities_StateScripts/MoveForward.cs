using UnityEngine;

namespace Cannon_Test
{
    [CreateAssetMenu(fileName = "New state", menuName = "Cannon_Test/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        [SerializeField] private float _speed;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ConstantMove(characterState.enemyControl, animator, stateInfo);
        }

        private void ConstantMove(EnemyControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.isMoving)
            {
                control.MoveForward(_speed);
            }
            else
            {
                animator.SetBool(control.walkingType.ToString(), false);
            }

        }
    }
}