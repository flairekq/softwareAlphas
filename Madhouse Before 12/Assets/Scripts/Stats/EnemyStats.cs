using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Animator animator;
    public float destroyDelay = 1.5f;
    public override void Die()
    {
        base.Die();
        // animator.SetBool("isChasing", false);
        // animator.SetInteger("attack", 0);
        //create an animation event and trigger the Die() after the end of the animation
        animator.SetBool("isDead", true);
        StartCoroutine(DestroyEnemy(destroyDelay));
        // Add death animation
        // animator.SetBool("isDead", true);

    }

    IEnumerator DestroyEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void DieDestroy(AnimationEvent animationEvent)
    {
        Debug.Log("destroying");
        Destroy(gameObject);
    }
}
