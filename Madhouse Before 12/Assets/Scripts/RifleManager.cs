using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManager : MonoBehaviour
{

    public int bulletspermag = 10;
    public int totalbulletsleft = 70;

    public int currentBullets; // number of bullets left in magazine 
    public float damage = 10f;
    public float range = 100f;
    public Transform shootPoint;
    //public CameraShake cameraShake;

    public ParticleSystem muzzleFlash;

    //private Animator anim;


    private int isShooting = Animator.StringToHash("isShooting");
   // private int isIdle = Animator.StringToHash("isIdle");


    private void Start()
    { 
        currentBullets = bulletspermag;
         //anim = GetComponent<Animator>();
    }

    void Update() {
         if(Input.GetMouseButtonDown(0)) {
           // Aim();
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentBullets <= 0 && totalbulletsleft>0) {
                //Reload();
                currentBullets = bulletspermag;
                totalbulletsleft -= bulletspermag;
            } else {
            Shoot();
            // StartCoroutine(cameraShake.Shake(0.15f, 0.05f));
            }
           
        }/* else {
            anim.SetBool("isIdle",true);
            anim.SetBool(isShooting, false);
            anim.SetBool("isReload", false);
        } */
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        currentBullets--;
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {   
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null) {
                target.TakeDamage(damage);
            }
        }
    } 
    
    }
  