using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManager : MonoBehaviour
{

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
        ParabolicBullet bulletScript = bullet.GetComponent<ParabolicBullet>();
        if(bulletScript) 
        {
            bulletScript.Initialize(shootPoint, shootSpeed, gravityForce);

        }
        Destroy(bullet, bulletLifeTime);
    }
}
