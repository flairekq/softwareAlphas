using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnProjectile : MonoBehaviour
{
    //public GameObject projectile;
    public GameObject firePoint;

    public float timeToFire = 0f;

    private PhotonView PV;

    
     private void Awake()
    {
       
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!PV.IsMine)
        {
            return;
        }
        
        if(Input.GetMouseButtonDown(0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / 4;
            SpawnBullet();
        }
        
    }

    void SpawnBullet()
    {
       // GameObject bullet;
        if(firePoint != null)
        {
           // bullet = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
           GameObject bullet =  bulletPooling.SharedInstance.GetPooledObject();
           bullet.SetActive(true);
           Rigidbody rb = bullet.GetComponent<Rigidbody>(); 
           TrailRenderer tr = bullet.GetComponent<TrailRenderer>();
           MeshRenderer mesh = GetComponent<MeshRenderer> ();
           rb.isKinematic = false;
           tr.enabled = true;
          // mesh.enabled = true;
           
           //rb.angularVelocity = Vector3.zero;
            bullet.transform.position = firePoint.transform.position;
            //bullet.transform.position += transform.forward * (15 * Time.deltaTime);
            bullet.transform.rotation = firePoint.transform.rotation;
            

        }
    }
}
