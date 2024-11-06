using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnStateEnter, updateOnStateExit = false;
    public bool updateOnStateMachineEnter, updateOnStateMachineExit = false;
    public float onEnterValue, onExitValue;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetFloat(floatName, onEnterValue);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(floatName, onExitValue);
        }
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
        {
            animator.SetFloat(floatName, onEnterValue);
        }
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetFloat(floatName, onExitValue);
        }
    }
}
