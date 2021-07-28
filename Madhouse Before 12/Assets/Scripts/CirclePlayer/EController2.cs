using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class EController2 : MonoBehaviour
{
    public GameObject player = null;
    public Transform model;
    public Transform proxy;
    public float lookRadius = 3.5f;
    public NavMeshAgent agent;
    NavMeshObstacle obstacle;
    Vector3 lastPosition;
    Animator animator;
    public AnimationClip[] attacks;
    private bool isChasing = false;
    public bool isAttacking = false;
    private bool isPatrolling = false;
    private bool isAttacked = false;
    private EnemyStats enemyStats;
    // int slot = -1;
    // private SlotManager slotManager;

    // public Transform[] moveSpots;
    public Transform moveSpot;
    // private int randomSpot;
    private float waitTime;
    private float toPathTime;
    public float startWaitTime;
    public float startToPathTime;
    EnvironmentManager envManager;
    GameController gameController;
    private float originalStoppingDistance;
    public int location;
    public PhotonView PV;
    // private float countdown = 0.4f;
    public int isChasingId;
    public int isPatrollingId;
    public int attackId;
    private float onMeshThreshold = 1f;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            isChasingId = Animator.StringToHash("isChasing");
            isPatrollingId = Animator.StringToHash("isPatrolling");
            attackId = Animator.StringToHash("attack");

            envManager = EnvironmentManager.instance;
            gameController = GameController.instance;

            agent = proxy.GetComponent<NavMeshAgent>();
            originalStoppingDistance = agent.stoppingDistance;
            obstacle = proxy.GetComponent<NavMeshObstacle>();
            // player = GameObject.FindGameObjectWithTag("Player");
            // slotManager = player.GetComponent<SlotManager>();
            animator = model.GetComponent<Animator>();
            enemyStats = model.GetComponentInChildren<EnemyStats>();
            // enemyStats = model.parent.GetComponent<EnemyStats>();

            // PositionRange range = envManager.basementPositionRange[Random.Range(0, envManager.basementPositionRange.Length)];
            // moveSpot.position = new Vector3(Random.Range(range.minX, range.maxX), gameObject.transform.position.y, Random.Range(range.minZ, range.maxZ));
            RandomnizeMoveSpot();
            waitTime = startWaitTime;
            toPathTime = startToPathTime;
        }
    }

    void Update()
    {
        if (GameController.instance.isGameOver)
        {
            this.enabled = false;
        }

        if (!PV.IsMine)
        {
            return;
        }

        if (!enemyStats.isDead)
        {
            // if (IsPlayerWithinDistance(slotManager))
            if ((player != null
                    // && Vector3.Distance(player.transform.position, proxy.position) <= lookRadius
                    && CheckIsNear()
                    && (location >= 3 || (location <= 2 && IsPlayerOnNavMesh())))
                // && IsPlayerOnNavMesh())
                || (player != null && isAttacked))
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
                agent.stoppingDistance = originalStoppingDistance;
                // agent.speed = 10f;
                isPatrolling = false;
                // Test if the distance between the agent (which is now the proxy) and the player
                // is less than the attack range (or the stoppingDistance parameter) 
                if (Vector3.Distance(player.transform.position, proxy.position) <= agent.stoppingDistance)
                {
                    // If the agent is in attack range, become an obstacle and 
                    // disable the NavMeshAgent component
                    agent.enabled = false;
                    obstacle.enabled = true;
                    isChasing = false;
                    isAttacked = false;
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
                        animator.SetInteger(attackId, 0);
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
                animator.SetInteger(attackId, 0);
                FindNearestPlayer();
                isChasing = false;
                isPatrolling = true;
                agent.stoppingDistance = 0;
                // agent.speed = 2f;

                // agent.SetDestination(moveSpot.position);
                // Debug.Log(Vector3.Distance(proxy.position, moveSpot.position));
                if (Vector3.Distance(proxy.position, moveSpot.position) < 0.2f)
                {
                    if (waitTime <= 0)
                    {
                        // PositionRange range = envManager.basementPositionRange[Random.Range(0, envManager.basementPositionRange.Length)];
                        // moveSpot.position = new Vector3(Random.Range(range.minX, range.maxX), gameObject.transform.position.y, Random.Range(range.minZ, range.maxZ));
                        RandomnizeMoveSpot();
                        waitTime = startWaitTime;
                        // patrol
                        obstacle.enabled = false;
                        agent.enabled = true;
                    }
                    else
                    {
                        agent.enabled = false;
                        obstacle.enabled = true;
                        isPatrolling = false;
                        waitTime -= Time.deltaTime;
                    }

                }
                else
                {
                    // patrol
                    obstacle.enabled = false;
                    agent.enabled = true;
                    if (toPathTime <= 0)
                    {
                        // PositionRange range = envManager.basementPositionRange[Random.Range(0, envManager.basementPositionRange.Length)];
                        // moveSpot.position = new Vector3(Random.Range(range.minX, range.maxX), gameObject.transform.position.y, Random.Range(range.minZ, range.maxZ));
                        RandomnizeMoveSpot();
                        toPathTime = startToPathTime;
                    }
                    else
                    {
                        toPathTime -= Time.deltaTime;
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
            }
            // This is needed to calculate the orientation in the next frame 
            lastPosition = model.position;

            animator.SetBool(isChasingId, isChasing);
            animator.SetBool(isPatrollingId, isPatrolling);
        }
    }

    public bool IsPlayerOnNavMesh()
    {
        NavMeshHit hit;

        // Check for nearest point on navmesh to agent, within onMeshThreshold
        Vector3 pos = player.transform.position;
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

    bool CheckIsNear() {
        if (Vector3.Distance(player.transform.position, proxy.position) <= lookRadius) {
            if (Vector3.Dot(proxy.up, player.transform.up) < 0) {
                return false;
            } else {
                return true;
            }
        }
        return false;
    }

    void FaceTarget()
    {
        // // Calculate the orientation based on the velocity of the agent 
        // Vector3 orientation = model.position - lastPosition;
        // // Check if the agent has some minimal velocity 
        // if (orientation.sqrMagnitude > 0.1f)
        // {
        //     Vector3 direction = (player.transform.position - model.position).normalized;
        //     direction.y = 0;
        //     // We don't want him to look up or down orientation.y = 0;
        //     // Use Quaternion.LookRotation() to set the model's new rotation and smooth the transition with Quaternion.Lerp();
        //     model.rotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 8);
        // }
        // else
        // {
        //     // If the agent is stationary we tell him to assume the players's rotation
        //     model.rotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(player.transform.up), Time.deltaTime * 8);
        // }
        // if (agent.velocity.magnitude <= 0.1)
        // {
        // Vector3 direction = player.transform.position - model.position;
        // direction.y = 0;
        // Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, model.position.y, direction.z));

        // Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // Quaternion lookRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0, player.transform.position.z));
        // model.rotation = Quaternion.Slerp(model.rotation, lookRotation, Time.deltaTime * 5f);
        // Vector3 lookAt = new Vector3(player.transform.position.x, , player.transform.position.z);
        model.LookAt(player.transform);
        // Vector3 direction = model.position - player.transform.position;
        // model.LookAt(direction);
        // model.rotation = Quaternion.LookRotation(player.transform.position - model.position, model.up);
        // model.rotation = Quaternion.RotateTowards(model.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 8);
        // }
    }

    void RandomnizeMoveSpot()
    {
        PositionRange range;
        // if (location.Equals("Basement"))
        if (location == 0) // basement
        {
            range = envManager.basementPositionRange[Random.Range(0, envManager.basementPositionRange.Length)];
        }
        // else if (location.Equals("FirstFloor"))
        else if (location == 1) // first floor 
        {
            range = envManager.firstFloorPositionRange[Random.Range(0, envManager.firstFloorPositionRange.Length)];
        }
        else if (location == 2) // 2nd floor
        {
            range = envManager.secondFloorPositionRange[Random.Range(0, envManager.secondFloorPositionRange.Length)];
        }
        else if (location == 3)
        { // classroom
            range = envManager.classroomPositionRange[Random.Range(0, envManager.classroomPositionRange.Length)];
        }
        else if (location == 4)
        { // bedroom
            range = envManager.bedroomPositionRange[Random.Range(0, envManager.bedroomPositionRange.Length)];
        }
        else
        { // dayroom
            range = envManager.dayroomPositionRange[Random.Range(0, envManager.dayroomPositionRange.Length)];
        }

        moveSpot.position = new Vector3(Random.Range(range.minX, range.maxX), gameObject.transform.position.y, Random.Range(range.minZ, range.maxZ));
    }

    void Attack()
    {
        int n = Random.Range(0, attacks.Length);
        animator.SetInteger(attackId, n + 1);
        AttackedPlayer();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(proxy.position, lookRadius);
    }

    void FindNearestPlayer()
    {
        float leastDistance = 1000000000f;
        foreach (GameObject p in gameController.gameObjectPlayers)
        {
            if (p != null)
            {
                float currentDistance = Vector3.Distance(p.transform.position, proxy.position);
                if (currentDistance < leastDistance)
                {
                    leastDistance = currentDistance;
                    player = p;
                }
            }
        }
    }

    // public void AttackedByPlayer(GameObject p)
    public void AttackedByPlayer(int pvID)
    {
        // Debug.Log("triggered on enemy side, isAttacking: " + isAttacking);
        if (!isAttacking)
        {
            PV.RPC("RPC_HandleAttackedByPlayer", RpcTarget.MasterClient, pvID);
            // Debug.Log(p.transform.position);
            // player = p;
            // isAttacked = true;
        }
    }

    [PunRPC]
    private void RPC_HandleAttackedByPlayer(int pvID)
    {
        // Debug.Log("calling rpc on master");
        GetPlayer(pvID);
        isAttacked = true;
    }

    void GetPlayer(int pvID)
    {
        foreach (GameObject p in gameController.gameObjectPlayers)
        {
            if (p != null)
            {
                if (p.GetPhotonView().ViewID == pvID)
                {
                    player = p;
                }
            }
        }
    }

    public void AttackedPlayer()
    {
        // Debug.Log("triggered attacked player");
        if (player != null)
        {
            // Debug.Log("trigger show canvas for player at: " + player.transform.position);
            player.GetComponent<PlayerStats>().Show(player.GetPhotonView());
        }
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

    public void StartDelayDeath()
    {
        StartCoroutine(DelayDeath(this));
    }

    IEnumerator DelayDeath(EController2 currentController)
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(currentController.PV);
    }
}