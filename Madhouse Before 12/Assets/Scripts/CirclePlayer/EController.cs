using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EController : MonoBehaviour
{
    public float lookRadius = 3f;

    GameObject target = null;
    float pathTime = 0f;
    int slot = -1;
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private SlotManager slotManager;
    private Rigidbody rb;
    private bool isCurrentlyOnSlot = false;
    private float delay = 0f;
    private Vector3 targetSlotPos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        slotManager = target.GetComponent<SlotManager>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        pathTime += Time.deltaTime;
        delay += Time.deltaTime;
        if (targetSlotPos != Vector3.zero)
        {
            delay += Time.deltaTime;
            if (delay > 0.5f)
            {
                delay = 0f;
                agent.enabled = true;
                agent.SetDestination(targetSlotPos);
                targetSlotPos = Vector3.zero;
            }
        }
        if (pathTime > 0.5f)
        {
            pathTime = 0f;
            if (IsPlayerWithinDistance(slotManager))
            {
                if (slotManager != null)
                {
                    // reserved a slot, check if the slot is still on navmesh
                    if (slot != -1 && !isCurrentlyOnSlot)
                    {
                        bool isOnNavMesh = slotManager.IsSlotOnNavMesh(slot);
                        if (!isOnNavMesh)
                        {
                            slotManager.Release(slot);
                            slot = -1;
                            isCurrentlyOnSlot = false;
                        }
                        else
                        {
                            if (slotManager.IsSlotStolen(gameObject, slot))
                            {
                                slot = -1;
                                isCurrentlyOnSlot = false;
                            }
                        }
                    }
                    else
                    {
                        if (slot == -1)
                            slot = slotManager.Reserve(gameObject);
                    }

                    // no slot available
                    if (slot == -1)
                    {
                        // shout
                        isCurrentlyOnSlot = false;
                        agent.enabled = false;
                        obstacle.enabled = true;
                    }
                    else
                    {
                        FaceTarget();

                        Vector3 slotPos = slotManager.GetSlotPosition(slot);
                        float distanceToSlot = Vector3.Distance(transform.position, slotPos);
                        // Debug.Log(gameObject.name + " distance to slot: " + distanceToSlot);
                        // chase
                        if (distanceToSlot > 0.8)
                        {
                            isCurrentlyOnSlot = false;
                            obstacle.enabled = false;
                            targetSlotPos = slotPos;
                            // agent.enabled = true;
                            // agent.SetDestination(slotPos);
                        }
                        else
                        {
                            isCurrentlyOnSlot = true;
                            // attack
                            agent.enabled = false;
                            obstacle.enabled = true;

                            Debug.Log(gameObject.name + " became obstacle");
                        }
                    }

                }
            }
            else
            {
                isCurrentlyOnSlot = false;
                // idle
                agent.enabled = false;
                obstacle.enabled = true;
            }

        }
    }

    bool IsPlayerWithinDistance(SlotManager s)
    {
        if (s != null)
        {
            for (int i = 0; i < s.count; i++)
            {
                float degreesPerIndex = 360f / s.count;
                var pos = target.transform.position;
                var offset = new Vector3(0f, 0f, s.distance);
                pos += (Quaternion.Euler(new Vector3(0f, degreesPerIndex * i, 0f)) * offset);
                if (Vector3.Distance(pos, transform.position) < lookRadius)
                {
                    return true;
                }
            }
        }
        return false;
    }
    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public virtual void Attack()
    {
        Debug.Log("Attacking");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
