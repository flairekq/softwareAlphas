using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachController : EnemyController
{
    public override void Attack()
    {
        int n = Random.Range(0, 3);
        if (n == 0)
        {
            animator.SetInteger("attack", 1);
        }
        else if (n == 1)
        {
            animator.SetInteger("attack", 2);
        }
        else
        {
            animator.SetInteger("attack", 3);
        }

        animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(gameObject.name + " attack length: " + animationLength);
        isAttacking = true;
    }
}
