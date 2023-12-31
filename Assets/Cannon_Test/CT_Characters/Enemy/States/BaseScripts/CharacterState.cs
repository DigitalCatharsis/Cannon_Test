using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon_Test
{

    public class CharacterState : StateMachineBehaviour
    {
        public EnemyControl enemyControl;

        public List<StateData> ListAbilityData = new List<StateData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (enemyControl == null) 
            {
                enemyControl = animator.transform.root.gameObject.GetComponent<EnemyControl>();
                enemyControl.CacheCharacterControl(animator);
            }

            foreach (StateData d in ListAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);
            }
        }
        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)    //this is a place where was fixed move forward x4 speed bug with uncessesary foreach
        {
                UpdateAll(this, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnExit(this, animator, stateInfo);
            }
        }
    }
}