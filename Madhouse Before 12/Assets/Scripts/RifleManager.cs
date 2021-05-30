using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManager : MonoBehaviour
{

    /* public int bulletspermag = 7;

     public int totalbulletsleft = 200;

     public int currentBullets; // current number of bullets in magazine 
     */

    public float damage = 10f;
    public float range = 100f;
    public Transform shootPoint;

    public Camera fpsCamera;

    public CameraShake cameraShake;

    public ParticleSystem muzzleFlash;
    public LayerMask enemyMask;

    private void Start()
    {
        //currentBullets = bulletspermag;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
            muzzleFlash.Play();
            StartCoroutine(cameraShake.Shake(0.15f, 0.05f));
            /*if(currentBullets > 0)
            {
            Shoot();
            StartCoroutine(cameraShake.Shake(0.15f, 0.05f));
            } else 
            {
                Reload();
            }
            */
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range, enemyMask))
        {
            // Debug.Log(hit.transform.name);
            // Target target = hit.transform.GetComponent<Target>();
            // if(target != null) {
            //     target.TakeDamage(damage);
            // }
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            // Debug.Log(hit.collider);
            if (interactable != null)
            {
                Debug.Log("player attacking " + interactable.gameObject.name);
                if (interactable is Enemy)
                {
                    Enemy e = (Enemy)interactable;
                    e.Interact();
                }
            }
        }
        //  currentBullets--;
    }

    /*    void Reload() 
        {
            currentBullets = bulletspermag; 
            totalbulletsleft -= bulletspermag;
        }
        */





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
