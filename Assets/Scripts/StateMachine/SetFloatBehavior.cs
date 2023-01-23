using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SetFloatBehavior : StateMachineBehaviour
{
    public string FloatName;
    public bool updateStateEnter, updateStateExit;
    public bool updateStateMachineEnter, updateStateMachineExit;
    public float valueOnEnter, valueOnExit;
    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateStateEnter)
        {
            animator.SetFloat(FloatName, valueOnEnter);
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateStateExit)
        {
            animator.SetFloat(FloatName, valueOnExit);
        }
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateStateMachineEnter)
        {
            animator.SetFloat(FloatName, valueOnEnter);
        }
    }
    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateStateMachineExit)
        {
            animator.SetFloat(FloatName, valueOnExit);
        }
    }
}
