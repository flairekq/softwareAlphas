using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    // Start
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //   Debug.Log(animator.gameObject.transform.parent.name + " attacking");
    // }

    // Update
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // Stops
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform current = animator.gameObject.transform.parent;
        EController2 currentController = current.GetComponent<EController2>();
        if (currentController.PV.IsMine)
        {
            // currentController.AttackedPlayer();
            TimerManagement.instance.AdjustTime(5, false);
            currentController.isAttacking = false;
        }
        // CharacterStats targetStats = currentController.player.GetComponent<CharacterStats>();
        // if (targetStats != null)
        // {
        //     animator.gameObject.GetComponentInChildren<CharacterCombat>().Attack(targetStats);
        // }

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
