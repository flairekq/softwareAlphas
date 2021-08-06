using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Destroy(animator.gameObject);
        // GameObject parent = animator.gameObject.transform.parent.gameObject;
        // if (parent.GetComponent<PhotonView>().IsMine) {
        // if (PhotonNetwork.IsMasterClient)
        // {
        EController2 currentController = animator.gameObject.transform.parent.GetComponent<EController2>();
        if (currentController.PV.IsMine)
        {
            // PhotonNetwork.Destroy(currentController.PV);
            TimerManagement.instance.AdjustTime(-30, true);
            GenerateEnemies.instance.EnemyKilled(currentController.location);
            // currentController.meshCollider.enabled = false;
            currentController.StartDelayDeath();
        }
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
