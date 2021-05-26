using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    public int startingCount = 5;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultipler = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultipler * avoidanceRadiusMultipler;

        for (int i = 0; i < startingCount; i++)
        {
            float xPos = Random.Range(21.9f, 23.9f);
            float zPos = Random.Range(-20.3f, -1.65f);
            Vector3 pos = new Vector3(xPos, 1.5f, zPos);
            // while (isTooClose(pos))
            // {
            //     xPos = Random.Range(21.9f, 23.9f);
            //     zPos = Random.Range(-20.3f, -1.65f);
            //     pos = new Vector3(xPos, 1.5f, zPos);
            // }

            FlockAgent newAgent = Instantiate(agentPrefab, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);

            newAgent.name = "Agent " + (i + 1);
            agents.Add(newAgent);
        }
    }

    bool isTooClose(Vector3 pos)
    {
        bool tooClose = false;
        foreach (FlockAgent agent in agents)
        {
            float distance = Vector3.Distance(pos, agent.transform.position);
            if (distance < 1.5f)
            {
                tooClose = true;
                break;
            }
        }
        return tooClose;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            move.y = 0f;
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
