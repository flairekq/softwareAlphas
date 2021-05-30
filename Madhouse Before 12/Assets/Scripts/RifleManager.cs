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

    public Transform shootPointIdle;
    public Camera fpsCamera;

    public CameraShake cameraShake;

    public ParticleSystem muzzleFlash;

    private Animator anim;

    public GameObject scopeOverlay;

    public GameObject weaponCamera;

    public Camera mainCamera;

    public float scopedFOV = 15f;
    private float normalFOV;

    //public GameObject impactEffect;

    private int isShooting = Animator.StringToHash("isShooting");
   // private int isIdle = Animator.StringToHash("isIdle");

    private bool isScoped= false;


    private void Start()
    { 
        currentBullets = bulletspermag;
         anim = GetComponent<Animator>();
    }

    void Update() {
         if(Input.GetMouseButtonDown(0)) {
            Aim();
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentBullets <= 0 && totalbulletsleft>0) {
                Reload();
                currentBullets = bulletspermag;
                totalbulletsleft -= bulletspermag;
            } else {
            Shoot();
             StartCoroutine(cameraShake.Shake(0.15f, 0.05f));
            }
           
        } else {
            anim.SetBool("isIdle",true);
            anim.SetBool(isShooting, false);
            anim.SetBool("isReload", false);
        } 
    }

    void Shoot() 
    {
        muzzleFlash.Play();
        RaycastHit hit;
        currentBullets--;
        if(isScoped) {
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {   
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null) {
                target.TakeDamage(damage);
            }
        }
        } else {
            if(Physics.Raycast(shootPointIdle.position, shootPointIdle.transform.forward, out hit, range))
        {   
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null) {
                target.TakeDamage(damage);
            }

            /*GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            */
        }
    }
    }
     private void Aim() 
    {   
        isScoped = !isScoped;
        anim.SetBool(isShooting, isScoped);

        if(isScoped) {
            StartCoroutine(OnScoped());
        } else {
            OnUnscoped();
        }
        
    }

    void OnUnscoped() 
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped() 
    {
        yield return new WaitForSeconds(0.15f);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = 15f;
    }

    void Reload()
    {
        
        if(isScoped) {
            isScoped = false; 
            OnUnscoped(); 
            anim.SetBool("isReload", true);
            anim.SetBool("isShooting", false);
            anim.SetBool("isIdle", false);
        } else {
            anim.SetBool("isReload", true);
            anim.SetBool("isShooting", false);
            anim.SetBool("isIdle", false);
        }
    }


}
