using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Animator animator;
    public float destroyDelay = 1.5f;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    public override void Die()
    {
        base.Die();
        isDead = true;
        // gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //create an animation event and trigger the Die() after the end of the animation
        // animator.enabled = false;
        // animator.enabled = true;
        // animator.Rebind();
        // gameObject.GetComponent<Animator>().enabled = false;
        // gameObject.GetComponent<Animator>().enabled = true;
        // gameObject.GetComponent<Animator>().SetBool("isDead", true);
        // animator.Play("Dead");
        animator.SetBool("isDead", true);
        StartCoroutine(DestroyEnemy(destroyDelay));
    }

    IEnumerator DestroyEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // public void DieDestroy(AnimationEvent animationEvent)
    // {
    //     Debug.Log("destroying");
    //     Destroy(gameObject);
    // }
}
