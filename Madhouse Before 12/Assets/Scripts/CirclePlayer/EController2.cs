using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EController2 : MonoBehaviour
{
    public Transform player;
    public Transform model;
    public Transform proxy;
    public float lookRadius = 3.5f;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    Vector3 lastPosition;
    void Start()
    {
        agent = proxy.GetComponent<NavMeshAgent>();
        obstacle = proxy.GetComponent<NavMeshObstacle>();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, proxy.position) <= lookRadius)
        {
            // Test if the distance between the agent (which is now the proxy) and the player
            // is less than the attack range (or the stoppingDistance parameter) 
            if (Vector3.Distance(player.position, proxy.position) <= agent.stoppingDistance)
            {
                // If the agent is in attack range, become an obstacle and 
                // disable the NavMeshAgent component
                agent.enabled = false;
                obstacle.enabled = true;
            }
            else
            {
                // If we are not in range, become an agent again 
                obstacle.enabled = false;
                agent.enabled = true;
                // And move to the player's position 
                agent.destination = player.position;
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

        }
        // This is needed to calculate the orientation in the next frame 
        lastPosition = model.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(proxy.position, lookRadius);
    }
}