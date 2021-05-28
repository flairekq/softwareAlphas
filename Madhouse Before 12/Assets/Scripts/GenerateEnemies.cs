using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public int noOfBasementEnemies;
    public int noOfFirstFloorEnemies;
    public int noOfSecondFloorEnemies;

    public List<EnemyToGenerate> basementEnemies;
    public List<EnemyToGenerate> firstFloorEnemies;
    public List<EnemyToGenerate> secondFloorEnemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop(basementEnemies, noOfBasementEnemies));
    }

    IEnumerator EnemyDrop(List<EnemyToGenerate> enemies, int maxNoOfEnemies)
    {
        int enemyCount = 0;
        while (enemyCount < maxNoOfEnemies)
        {
            int i = Random.Range(0, enemies.Count);
            float xPos = Random.Range(22.74f, 23.9f);
            float zPos = Random.Range(-20.3f, -1.65f);
            Instantiate(enemies[i].enemy, new Vector3(xPos, enemies[i].yPos, zPos), Quaternion.identity);
            // yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}
