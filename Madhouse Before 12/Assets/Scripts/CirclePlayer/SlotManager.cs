using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlotManager : MonoBehaviour
{
    private List<GameObject> slots;
    public int count = 3;
    public float distance = 2.3f;
    float onMeshThreshold = 1f;

    void Start()
    {
        slots = new List<GameObject>();
        for (int index = 0; index < count; ++index)
        {
            slots.Add(null);
        }
    }

    public Vector3 GetSlotPosition(int index)
    {
        float degreesPerIndex = 360f / count;
        var pos = transform.position;
        var offset = new Vector3(0f, 0f, distance);
        return pos + (Quaternion.Euler(new Vector3(0f, degreesPerIndex * index, 0f)) * offset);
    }

    public int Reserve(GameObject attacker)
    {
        var bestPosition = transform.position;
        var offset = (attacker.transform.position - bestPosition).normalized * distance;
        bestPosition += offset;
        int bestSlot = -1;
        float bestDist = 99999f;
        for (int index = 0; index < slots.Count; ++index)
        {
            if (slots[index] != null)
                continue;
            if (IsSlotOnNavMesh(index))
            {
                var dist = (GetSlotPosition(index) - bestPosition).sqrMagnitude;

                if (dist < bestDist)
                {
                    bestSlot = index;
                    bestDist = dist;
                }
            }
        }
        if (bestSlot != -1)
            slots[bestSlot] = attacker;
        return bestSlot;
    }

    public bool IsSlotOnNavMesh(int index)
    {
        NavMeshHit hit;
        Vector3 pos = GetSlotPosition(index);

        // Check for nearest point on navmesh to agent, within onMeshThreshold
        if (NavMesh.SamplePosition(pos, out hit, onMeshThreshold, NavMesh.AllAreas))
        {
            // Check if the positions are vertically aligned
            if (Mathf.Approximately(pos.x, hit.position.x)
                && Mathf.Approximately(pos.z, hit.position.z))
            {
                // Lastly, check if object is below navmesh
                return pos.y >= hit.position.y;
            }
        }
        return false;
    }

    public bool IsSlotStolen(GameObject attacker, int index)
    {
        if (slots[index] != attacker)
        {
            return true;
        }
        return false;
    }

    public void Release(int slot)
    {
        slots[slot] = null;
    }

    void OnDrawGizmosSelected()
    {
        for (int index = 0; index < count; ++index)
        {
            // if (slots == null || slots.Count <= index || slots[index] == null)
            //     Gizmos.color = Color.black;
            // else
            //     Gizmos.color = Color.red;
            // Gizmos.DrawWireSphere(GetSlotPosition(index), 0.5f);

            if (slots == null || slots.Count <= index || (slots[index] == null && IsSlotOnNavMesh(index)))
            {
                Gizmos.color = Color.green;
            }
            else if (slots[index] != null)
            {
                Gizmos.color = Color.black;
            }
            else if (!IsSlotOnNavMesh(index))
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawWireSphere(GetSlotPosition(index), 0.5f);
        }
    }
}
