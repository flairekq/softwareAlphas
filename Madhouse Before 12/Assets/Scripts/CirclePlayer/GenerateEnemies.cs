using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GenerateEnemies : MonoBehaviour
{
    public static GenerateEnemies instance;
    public int noOfBasementEnemies;
    public int noOfFirstFloorEnemies;
    public int noOfSecondFloorEnemies;
    public int noOfClassroomEnemies;
    public int noOfBedroomEnemies;
    public int noOfDayroomEnemies;
    private int noOfRemainingBasementEnemies;
    private int noOfRemainingFirstFloorEnemies;
    private int noOfRemainingSecondFloorEnemies;
    private int noOfRemainingClassroomEnemies;
    private int noOfRemainingBedroomEnemies;
    private int noOfRemainingDayroomEnemies;
    private float basementCT = 10f;
    private float firstFloorCT = 10f;
    private float secondFloorCT = 10f;
    private float classroomCT = 10f;
    private float bedroomCT = 10f;
    private float dayroomCT = 10f;

    public List<EnemyToGenerate> basementEnemies;
    public List<EnemyToGenerate> firstFloorEnemies;
    public List<EnemyToGenerate> secondFloorEnemies;
    public List<EnemyToGenerate> classroomEnemies;
    public List<EnemyToGenerate> bedroomEnemies;
    public List<EnemyToGenerate> dayroomEnemies;
    GameObject moveSpot;
    EnvironmentManager envManager;

    // private float countdownTimer = 3f;
    // private bool generated = false;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instance.envManager = EnvironmentManager.instance;
        instance.PV = GetComponent<PhotonView>();
        instance.noOfRemainingBasementEnemies = noOfBasementEnemies;
        instance.noOfRemainingFirstFloorEnemies = noOfFirstFloorEnemies;
        instance.noOfRemainingSecondFloorEnemies = noOfSecondFloorEnemies;
        instance.noOfRemainingClassroomEnemies = noOfClassroomEnemies;
        instance.noOfRemainingBedroomEnemies = noOfBedroomEnemies;
        instance.noOfRemainingDayroomEnemies = noOfDayroomEnemies;
        // StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, "Basement"));
        // StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, "FirstFloor"));
        // StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, "SecondFloor"));
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (GenerateEnemies.instance.noOfRemainingBasementEnemies <= 0)
        {
            if (basementCT <= 0f)
            {
                basementCT = 10f;
                GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 0);
                StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, 0));
            }
            else
            {
                basementCT -= Time.deltaTime;
            }
        }

        if (GenerateEnemies.instance.noOfRemainingFirstFloorEnemies <= 0)
        {
            if (firstFloorCT <= 0f)
            {
                firstFloorCT = 10f;
                GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 1);
                StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, 1));
            }
            else
            {
                firstFloorCT -= Time.deltaTime;
            }
        }

        if (GenerateEnemies.instance.noOfRemainingSecondFloorEnemies <= 0)
        {
            if (secondFloorCT <= 0f)
            {
                secondFloorCT = 10f;
                GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 2);
                StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, 2));
            }
            else
            {
                secondFloorCT -= Time.deltaTime;
            }
        }

        if (GenerateEnemies.instance.noOfRemainingClassroomEnemies <= 0)
        {
            if (classroomCT <= 0f)
            {
                classroomCT = 10f;
                GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 3);
                StartCoroutine(EnemyDrop(classroomEnemies, noOfClassroomEnemies, envManager.classroomPositionRange, 3));
            }
            else
            {
                classroomCT -= Time.deltaTime;
            }
        }

        if (GenerateEnemies.instance.noOfRemainingBedroomEnemies <= 0)
        {
            if (bedroomCT <= 0f)
            {
                bedroomCT = 10f;
                GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 4);
                StartCoroutine(EnemyDrop(bedroomEnemies, noOfBedroomEnemies, envManager.bedroomPositionRange, 4));
            }
            else
            {
                bedroomCT -= Time.deltaTime;
            }
        }

        if (GenerateEnemies.instance.noOfRemainingDayroomEnemies <= 0)
        {
            if (dayroomCT <= 0f)
            {
                dayroomCT = 10f;
                GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 5);
                StartCoroutine(EnemyDrop(dayroomEnemies, noOfDayroomEnemies, envManager.dayroomPositionRange, 5));
            }
            else
            {
                dayroomCT -= Time.deltaTime;
            }
        }
    }

    public void InitialSpawnEnemies()
    {
        // GenerateEnemies.instance.PV.RPC("RPC_HandleInitialSpawnEnemies", RpcTarget.All);
        // Debug.Log("triggered");

        GameController.instance.GetAllPlayers();
        StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, 0));
        StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, 1));
        StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, 2));
        StartCoroutine(EnemyDrop(classroomEnemies, noOfClassroomEnemies, envManager.classroomPositionRange, 3));
        StartCoroutine(EnemyDrop(bedroomEnemies, noOfBedroomEnemies, envManager.bedroomPositionRange, 4));
        StartCoroutine(EnemyDrop(dayroomEnemies, noOfDayroomEnemies, envManager.dayroomPositionRange, 5));
    }

    // [PunRPC]
    // private void RPC_HandleInitialSpawnEnemies()
    // {
    //     GameController.instance.GetAllPlayers();
    //     StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, 0));
    //     StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, 1));
    //     StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, 2));
    //     StartCoroutine(EnemyDrop(classroomEnemies, noOfClassroomEnemies, envManager.classroomPositionRange, 3));
    //     StartCoroutine(EnemyDrop(bedroomEnemies, noOfBedroomEnemies, envManager.bedroomPositionRange, 4));
    //     StartCoroutine(EnemyDrop(dayroomEnemies, noOfDayroomEnemies, envManager.dayroomPositionRange, 5));
    // }

    public void EnemyKilled(int location)
    {
        GenerateEnemies.instance.PV.RPC("RPC_HandleEnemyKilled", RpcTarget.All, location);
    }

    [PunRPC]
    private void RPC_HandleEnemyKilled(int location)
    {
        switch (location)
        {
            case 0:
                GenerateEnemies.instance.noOfRemainingBasementEnemies -= 1;
                break;
            case 1:
                GenerateEnemies.instance.noOfRemainingFirstFloorEnemies -= 1;
                break;
            case 2:
                GenerateEnemies.instance.noOfRemainingSecondFloorEnemies -= 1;
                break;
            case 3:
                GenerateEnemies.instance.noOfRemainingClassroomEnemies -= 1;
                break;
            case 4:
                GenerateEnemies.instance.noOfRemainingBedroomEnemies -= 1;
                break;
            case 5:
                GenerateEnemies.instance.noOfRemainingDayroomEnemies -= 1;
                break;
            default:
                break;
        }
    }

    [PunRPC]
    private void RPC_HandleRespawn(int location)
    {
        switch (location)
        {
            case 0:
                GenerateEnemies.instance.noOfRemainingBasementEnemies = noOfBasementEnemies;
                break;
            case 1:
                GenerateEnemies.instance.noOfRemainingFirstFloorEnemies = noOfFirstFloorEnemies;
                break;
            case 2:
                GenerateEnemies.instance.noOfRemainingSecondFloorEnemies = noOfSecondFloorEnemies;
                break;
            case 3:
                GenerateEnemies.instance.noOfRemainingClassroomEnemies = noOfClassroomEnemies;
                break;
            case 4:
                GenerateEnemies.instance.noOfRemainingBedroomEnemies = noOfBedroomEnemies;
                break;
            case 5:
                GenerateEnemies.instance.noOfRemainingDayroomEnemies = noOfDayroomEnemies;
                break;
            default:
                break;
        }
    }

    IEnumerator EnemyDrop(List<EnemyToGenerate> enemies, int maxNoOfEnemies, PositionRange[] positionsRange, int location)
    {
        int enemyCount = 0;
        while (enemyCount < maxNoOfEnemies)
        {
            int i = Random.Range(0, enemies.Count);
            int rangeIndex;
            float xPos;
            float zPos;
            Vector3 enemyPos = Vector3.zero;
            // int rangeIndex = Random.Range(0, positionsRange.Length);
            // float xPos = Random.Range(positionsRange[rangeIndex].minX, positionsRange[rangeIndex].maxX);
            // float zPos = Random.Range(positionsRange[rangeIndex].minZ, positionsRange[rangeIndex].maxZ);
            // Vector3 enemyPos = new Vector3(xPos, enemies[i].yPos, zPos);
            bool isNearAnyone = true;
            int failureCount = 0;
            while (isNearAnyone && failureCount < 5)
            {
                rangeIndex = Random.Range(0, positionsRange.Length);
                xPos = Random.Range(positionsRange[rangeIndex].minX, positionsRange[rangeIndex].maxX);
                zPos = Random.Range(positionsRange[rangeIndex].minZ, positionsRange[rangeIndex].maxZ);
                enemyPos = new Vector3(xPos, enemies[i].yPos, zPos);

                isNearAnyone = false;
                foreach (GameObject p in GameController.instance.gameObjectPlayers)
                {
                    if (p != null && Vector3.Distance(p.transform.position, enemyPos) <= 2.5)
                    {
                        failureCount++;
                        isNearAnyone = true;
                        break;
                    }
                }
            }
            GameObject enemy = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", enemies[i].prefabName), enemyPos, Quaternion.identity);
            EController2 enemyController = enemy.GetComponent<EController2>();
            enemyController.location = location;
            enemyController.moveSpot = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "MoveSpot"), enemyPos, Quaternion.identity).transform;
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}
