using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManager : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f; 
    public Transform shootPoint;

    public Camera fpsCamera;

    public CameraShake cameraShake;

    void Update() {
        if(Input.GetMouseButtonDown(1))
        {
            Shoot();
            StartCoroutine(cameraShake.Shake(0.15f, 0.05f));
        }
    }

    void Shoot() 
    {
        RaycastHit hit;
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null) {
                target.TakeDamage(damage);
            }
        }
    }






    /*
    public GameObject bulletProf;
    public Transform shootPoint;
    float T = 0;
    float reloadTime = 1f;

    [Space]
    public float shootSpeed;
    public float gravityForce;
    public float bulletLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            Shoot();
        }
        
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletProf, shootPoint.position, shootPoint.rotation);
       // bullet.transform.forward = shootPoint.transform.forward;
        ParabolicBullet bulletScript = bullet.GetComponent<ParabolicBullet>();
        if(bulletScript) 
        {
            bulletScript.Initialize(shootPoint, shootSpeed, gravityForce);

        }
        Destroy(bullet, bulletLifeTime);
    } */
}
