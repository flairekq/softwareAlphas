using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    public Animator animator;
    protected Transform target;
    protected NavMeshAgent agent;
    protected CharacterCombat combat;
    protected EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stats.isDead)
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
        } else {
            agent.isStopped = true;
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

    public virtual void Attack()
    {
        Debug.Log("Attacking");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
