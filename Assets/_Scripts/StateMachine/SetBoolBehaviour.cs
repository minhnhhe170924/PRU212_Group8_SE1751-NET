using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string boolName;
    public bool updateOnState = false;
    public bool updateOnStateMachine = false;
    public bool onEnterValue, onExitValue;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, onEnterValue);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, onExitValue);
            if(stateInfo.IsName("player_switch_gravity"))
            {
                animator.gameObject.transform.localScale *= new Vector2(1, -1);
            }
        }
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, onEnterValue);
        }
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, onExitValue);
        }
    }
}
