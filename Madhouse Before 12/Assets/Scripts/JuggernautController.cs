using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggernautController : EnemyController
{
    public override void Attack()
    {
        int n = Random.Range(0, 6);
        if (n == 0)
        {
            animator.SetInteger("attack", 1);
        }
        else if (n == 1)
        {
            animator.SetInteger("attack", 2);
        }
        else if (n == 2)
        {
            animator.SetInteger("attack", 3);
        }
        else if (n == 3)
        {
            animator.SetInteger("attack", 4);
        }
        else if (n == 4)
        {
            animator.SetInteger("attack", 5);
        }
        else
        {
            animator.SetInteger("attack", 6);
        }
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        if (targetStats != null)
        {
            combat.Attack(targetStats);
        }
    }
}
