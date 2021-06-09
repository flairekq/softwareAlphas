using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    protected Animator animator;
    protected Transform target;
    protected NavMeshAgent agent;
    // protected NavMeshObstacle obstacle;
    protected CharacterCombat combat;
    protected EnemyStats stats;
    private bool isChasing = false;
    // private bool isDoneAttacking = true;
    protected float animationLength = 0f;
    protected bool isAttacking = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManagerOld.instance.player.transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        // obstacle = GetComponent<NavMeshObstacle>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();

        // 0 being the most impt
        // 99 being least impt
        // int avoidancePriority = Random.Range(0, 100);
        // agent.avoidancePriority = avoidancePriority;
        // obstacle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stats.isDead)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance < lookRadius)
            {
                // if (!isChasing)
                // {
                //     isChasing = true;
                //     animator.SetBool("isChasing", true);
                // }
                // else if (isChasing)
                // {
                //     // Debug.Log(distance);
                //     agent.SetDestination(target.position);
                // }

                // if (!isAttacking)
                // {

                //     agent.SetDestination(target.position);
                // }
                if (!isChasing) {
                    agent.SetDestination(target.position);
                    if (agent.hasPath && agent.pathStatus == NavMeshPathStatus.PathComplete) {
                        isChasing = true;
                        animator.SetBool("isChasing", true);
                    }
                }
                FaceTarget();

                if (distance <= agent.stoppingDistance)
                {
                    if (isChasing)
                    {
                        isChasing = false;
                        animator.SetBool("isChasing", false);
                    }

                    if (animationLength <= 0)
                    {
                        if (isAttacking)
                        {
                            isAttacking = false;
                            CharacterStats targetStats = target.GetComponent<CharacterStats>();
                            if (targetStats != null)
                            {
                                combat.Attack(targetStats);
                            }
                        }
                        Attack();
                    }
                    else
                    {
                        animationLength -= Time.deltaTime;
                    }
                }
            }
            else if (isChasing)
            {
                animator.SetBool("isChasing", false);
                animator.SetInteger("attack", 0);
                isChasing = false;
                isAttacking = false;
            }
            else
            {
            }

            // if (isChasing && !agent.hasPath && agent.pathStatus == NavMeshPathStatus.PathComplete) {
            //     Debug.Log("character stuck");
            //     agent.enabled = false;  
            //     agent.enabled = true;
            //     Debug.Log("navmesh re-enabled");
            // }
        }
        else
        {
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
