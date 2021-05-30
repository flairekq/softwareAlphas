using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject Foodprefab;
    public Vector3 center;
    public Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        spawnFood();
    }

    // Update is called once per frame
    void Update()
    {
        //if need to respawn
    }

    public void spawnFood()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x/2), 
       Random.Range(-size.y / 2, size.y/2));

       Instantiate(Foodprefab, pos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(center, size);
    }
}
