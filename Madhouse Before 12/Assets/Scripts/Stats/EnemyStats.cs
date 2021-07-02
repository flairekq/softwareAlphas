using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Animator animator;
    private void Start() {
        animator = gameObject.transform.parent.GetComponent<Animator>();
        // animator = GetComponent<Animator>();
    }
    
    public override void Die()
    {
        // base.Die();
        isDead = true;
        animator.SetBool("isDead", true);
    }
}
