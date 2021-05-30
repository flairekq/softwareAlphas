using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EController2 : MonoBehaviour
{
    public GameObject player = null;
    public Transform model;
    public Transform proxy;
    public float lookRadius = 3.5f;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    Vector3 lastPosition;
    Animator animator;
    public AnimationClip[] attacks;
    private bool isChasing = false;
    public bool isAttacking = false;
    private EnemyStats enemyStats;
    // int slot = -1;
    // private SlotManager slotManager;

    void Start()
    {
        agent = proxy.GetComponent<NavMeshAgent>();
        obstacle = proxy.GetComponent<NavMeshObstacle>();
        player = GameObject.FindGameObjectWithTag("Player");
        // slotManager = player.GetComponent<SlotManager>();
        animator = model.GetComponent<Animator>();
        enemyStats = model.GetComponentInChildren<EnemyStats>();
    }

    void Update()
    {
        if (!enemyStats.isDead)
        {
            // if (IsPlayerWithinDistance(slotManager))
            if (Vector3.Distance(player.transform.position, proxy.position) <= lookRadius)
            {
                #region Attack Slot System
                // // check if reserved slot is still on navmesh
                // if (slot != -1)
                // {
                //     bool isOnNavMesh = slotManager.IsSlotOnNavMesh(slot);
                //     if (!isOnNavMesh)
                //     {
                //         slotManager.Release(slot);
                //         slot = -1;
                //     }
                //     else
                //     {
                //         if (slotManager.IsSlotStolen(gameObject, slot))
                //         {
                //             slot = -1;
                //         }
                //     }
                // }
                // else
                // {
                //     slot = slotManager.Reserve(gameObject);
                // }

                // // no slot available 
                // if (slot == -1)
                // {
                //     agent.enabled = false;
                //     obstacle.enabled = true;
                //     isChasing = false;
                // }
                // else
                // {
                //     Vector3 slotPos = slotManager.GetSlotPosition(slot);
                //     float distanceToSlot = Vector3.Distance(proxy.position, slotPos);
                //     Debug.Log(distanceToSlot);
                //     if (distanceToSlot > 0.5)
                //     {
                //         // If we are not in range, become an agent again 
                //         obstacle.enabled = false;
                //         agent.enabled = true;
                //         // And move to the slot's position 
                //         agent.destination = slotPos;
                //         isChasing = true;
                //     }
                //     else
                //     {
                //         // If the agent is in attack range, become an obstacle and 
                //         // disable the NavMeshAgent component
                //         agent.enabled = false;
                //         obstacle.enabled = true;
                //         isChasing = false;
                //     }
                // }
                #endregion

                // Test if the distance between the agent (which is now the proxy) and the player
                // is less than the attack range (or the stoppingDistance parameter) 
                if (Vector3.Distance(player.transform.position, proxy.position) <= agent.stoppingDistance)
                {
                    // If the agent is in attack range, become an obstacle and 
                    // disable the NavMeshAgent component
                    agent.enabled = false;
                    obstacle.enabled = true;
                    isChasing = false;
                    if (!isAttacking)
                    {
                        Attack();
                    }
                }
                else
                {
                    if (!isAttacking)
                    {
                        // If we are not in range, become an agent again 
                        obstacle.enabled = false;
                        agent.enabled = true;
                        // And move to the player's position (will be handled when chasing animation starts to play)
                        // agent.SetDestination(player.transform.position);
                        isChasing = true;
                        animator.SetInteger("attack", 0);
                    }
                }
                model.position = Vector3.Lerp(model.position, proxy.position, Time.deltaTime * 2);

                // Calculate the orientation based on the velocity of the agent 
                Vector3 orientation = model.position - lastPosition;
                // Check if the agent has some minimal velocity 
                if (orientation.sqrMagnitude > 0.1f)
                {
                    // We don't want him to look up or down orientation.y = 0;
                    // Use Quaternion.LookRotation() to set the model's new rotation and smooth the transition with Quaternion.Lerp();
                    model.rotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(model.position - lastPosition), Time.deltaTime * 8);
                }
                else
                {
                    // If the agent is stationary we tell him to assume the proxy's rotation
                    model.rotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(proxy.forward), Time.deltaTime * 8);
                }

                FaceTarget();

            }
            else
            {
                isChasing = false;
                animator.SetInteger("attack", 0);
            }
            // This is needed to calculate the orientation in the next frame 
            lastPosition = model.position;

            if (isChasing)
            {
                animator.SetBool("isChasing", isChasing);
            }
            else
            {
                animator.SetBool("isChasing", isChasing);
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - model.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        model.rotation = Quaternion.Slerp(model.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Attack()
    {
        int n = Random.Range(0, attacks.Length);
        animator.SetInteger("attack", n + 1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(proxy.position, lookRadius);
    }

    // bool IsPlayerWithinDistance(SlotManager s)
    // {
    //     if (s != null)
    //     {
    //         for (int i = 0; i < s.count; i++)
    //         {
    //             float degreesPerIndex = 360f / s.count;
    //             var pos = player.transform.position;
    //             var offset = new Vector3(0f, 0f, s.distance);
    //             pos += (Quaternion.Euler(new Vector3(0f, degreesPerIndex * i, 0f)) * offset);
    //             if (Vector3.Distance(pos, proxy.position) < lookRadius)
    //             {
    //                 return true;
    //             }
    //         }
    //     }
    //     return false;
    // }
}