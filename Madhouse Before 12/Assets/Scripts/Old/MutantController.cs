using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : EnemyController
{
    public override void Attack()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        // Debug.Log("distance: " + distance);
        if (distance >= 3)
        {
            animator.SetInteger("attack", 2);
        }
        else
        {
            int n = Random.Range(0, 2);
            if (n == 0)
            {
                animator.SetInteger("attack", 1);
            }
            else
            {
                animator.SetInteger("attack", 4);
            }
        }

        animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(gameObject.name + " attack length: " + animationLength);
        isAttacking = true;
    }
}
