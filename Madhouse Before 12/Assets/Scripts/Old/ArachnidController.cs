using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArachnidController : EnemyController
{
    public override void Attack()
    {
        int n = Random.Range(0, 2);
        if (n == 0)
        {
            animator.SetInteger("attack", 1);
        }
        else
        {
            animator.SetInteger("attack", 2);
        }

        animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        // Debug.Log(gameObject.name + " attack length: " + animationLength);
        isAttacking = true;
        // CharacterStats targetStats = target.GetComponent<CharacterStats>();
        // if (targetStats != null)
        // {
        //     combat.Attack(targetStats, animationLength);
        // }
    }
}
