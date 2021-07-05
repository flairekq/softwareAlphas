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

    public List<EnemyToGenerate> basementEnemies;
    public List<EnemyToGenerate> firstFloorEnemies;
    public List<EnemyToGenerate> secondFloorEnemies;
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

        if (countdownTimer <= 0 && !generated)
        {
            GameController.instance.GetAllPlayers();
            StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies, envManager.basementPositionRange, "Basement"));
            StartCoroutine(EnemyDrop(firstFloorEnemies, noOfFirstFloorEnemies, envManager.firstFloorPositionRange, "FirstFloor"));
            StartCoroutine(EnemyDrop(secondFloorEnemies, noOfSecondFloorEnemies, envManager.secondFloorPositionRange, "SecondFloor"));
            generated = true;
        }
        else
        {
            countdownTimer -= Time.deltaTime;
        }
    }

    IEnumerator EnemyDrop(List<EnemyToGenerate> enemies, int maxNoOfEnemies, PositionRange[] positionsRange, string location)
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
