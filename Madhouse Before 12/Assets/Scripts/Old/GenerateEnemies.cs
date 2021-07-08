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

    public List<EnemyToGenerate> basementEnemies;
    public List<EnemyToGenerate> firstFloorEnemies;
    public List<EnemyToGenerate> secondFloorEnemies;
    public List<EnemyToGenerate> classroomEnemies;
    public List<EnemyToGenerate> bedroomEnemies;
    public List<EnemyToGenerate> dayroomEnemies;
    GameObject moveSpot;
    EnvironmentManager envManager;

    private float countdownTimer = 3f;
    private bool generated = false;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // instance.moveSpot = new GameObject();
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

        // if (countdownTimer <= 0 && !generated)
        // {
        //     GameController.instance.GetAllPlayers();
        //     StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, "Basement"));
        //     StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, "FirstFloor"));
        //     StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, "SecondFloor"));
        //     generated = true;
        // }
        // else
        // {
        //     countdownTimer -= Time.deltaTime;
        // }

        if (GenerateEnemies.instance.noOfRemainingBasementEnemies <= 0)
        {
            GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 0);
            StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, 0));
        }

        if (GenerateEnemies.instance.noOfRemainingFirstFloorEnemies <= 0)
        {
            GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 1);
            StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, 1));
        }

        if (GenerateEnemies.instance.noOfRemainingSecondFloorEnemies <= 0)
        {
            GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 2);
            StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, 2));
        }

        if (GenerateEnemies.instance.noOfRemainingClassroomEnemies <= 0)
        {
            GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 3);
            StartCoroutine(EnemyDrop(classroomEnemies, noOfClassroomEnemies, envManager.classroomPositionRange, 3));
        }

        if (GenerateEnemies.instance.noOfRemainingBedroomEnemies <= 0)
        {
            GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 4);
            StartCoroutine(EnemyDrop(bedroomEnemies, noOfBedroomEnemies, envManager.bedroomPositionRange, 4));
        }

        if (GenerateEnemies.instance.noOfRemainingDayroomEnemies <= 0)
        {
            GenerateEnemies.instance.PV.RPC("RPC_HandleRespawn", RpcTarget.All, 5);
            StartCoroutine(EnemyDrop(dayroomEnemies, noOfDayroomEnemies, envManager.dayroomPositionRange, 5));
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
                // if (GenerateEnemies.instance.noOfRemainingBasementEnemies <= 0)
                // {
                //     GenerateEnemies.instance.noOfRemainingBasementEnemies = noOfBasementEnemies;
                //     StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, 0));
                // }
                break;
            case 1:
                GenerateEnemies.instance.noOfRemainingFirstFloorEnemies -= 1;
                // if (GenerateEnemies.instance.noOfRemainingFirstFloorEnemies <= 0)
                // {
                //     GenerateEnemies.instance.noOfRemainingFirstFloorEnemies = noOfFirstFloorEnemies;
                //     StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, 1));
                // }
                break;
            case 2:
                GenerateEnemies.instance.noOfRemainingSecondFloorEnemies -= 1;
                // if (GenerateEnemies.instance.noOfRemainingSecondFloorEnemies <= 0)
                // {
                //     GenerateEnemies.instance.noOfRemainingSecondFloorEnemies = noOfSecondFloorEnemies;
                //     StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, 2));
                // }
                break;
            case 3:
                GenerateEnemies.instance.noOfRemainingClassroomEnemies -= 1;
                // if (GenerateEnemies.instance.noOfRemainingClassroomEnemies <= 0)
                // {
                //     GenerateEnemies.instance.noOfRemainingClassroomEnemies = noOfClassroomEnemies;
                //     StartCoroutine(EnemyDrop(classroomEnemies, noOfClassroomEnemies, envManager.classroomPositionRange, 3));
                // }
                break;
            case 4:
                GenerateEnemies.instance.noOfRemainingBedroomEnemies -= 1;
                // if (GenerateEnemies.instance.noOfRemainingBedroomEnemies <= 0)
                // {
                //     GenerateEnemies.instance.noOfRemainingBedroomEnemies = noOfBedroomEnemies;
                //     StartCoroutine(EnemyDrop(bedroomEnemies, noOfBedroomEnemies, envManager.bedroomPositionRange, 4));
                // }
                break;
            case 5:
                GenerateEnemies.instance.noOfRemainingDayroomEnemies -= 1;
                // if (GenerateEnemies.instance.noOfRemainingDayroomEnemies <= 0)
                // {
                //     GenerateEnemies.instance.noOfRemainingDayroomEnemies = noOfDayroomEnemies;
                //     StartCoroutine(EnemyDrop(dayroomEnemies, noOfDayroomEnemies, envManager.dayroomPositionRange, 5));
                // }
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
            int rangeIndex = Random.Range(0, positionsRange.Length);
            float xPos = Random.Range(positionsRange[rangeIndex].minX, positionsRange[rangeIndex].maxX);
            float zPos = Random.Range(positionsRange[rangeIndex].minZ, positionsRange[rangeIndex].maxZ);
            // GameObject enemy = Instantiate(enemies[i].enemy, new Vector3(xPos, enemies[i].yPos, zPos), Quaternion.identity);
            GameObject enemy = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", enemies[i].prefabName), new Vector3(xPos, enemies[i].yPos, zPos), Quaternion.identity);

            enemy.GetComponent<EController2>().location = location;
            enemy.GetComponent<EController2>().moveSpot = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MoveSpot"), new Vector3(xPos, enemies[i].yPos, zPos), Quaternion.identity).transform;
            // yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}
