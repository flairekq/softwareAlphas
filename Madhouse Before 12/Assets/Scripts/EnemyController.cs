using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    public Animator animator;
    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance < lookRadius)
        {
            // Debug.Log(distance);
            agent.SetDestination(target.position);
            animator.SetBool("isChasing", true);
            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("isChasing", false);
                // attack the target
                Attack();
                // face the target
                FaceTarget();
            }
        } 
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        // Vector3 direction = (target.position - transform.position).normalized;
        // transform.rotation = Quaternion.LookRotation(direction);
    }

    void Attack()
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
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        if (targetStats != null) {
            combat.Attack(targetStats);
        }    
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
