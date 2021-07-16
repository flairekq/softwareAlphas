using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject projectile;
    public GameObject firePoint;

    public float timeToFire = 0f;

    
    //private GameObject effectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / 4;
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        GameObject bullet;
        if(firePoint != null)
        {
            bullet = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
            

        }
    }
}
