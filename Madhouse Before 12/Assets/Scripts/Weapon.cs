using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{

    /*public int bulletsPerMag = 7;

     public int totalbulletsleft = 200;

     public int currentBullets; // current number of bullets in magazine 
     public float fireRate = 0.1f;
     private float fireTimer;
     */

    private Animator anim;



    public ParticleSystem muzzleFlash;

    public GameObject scopeOverlay;

    public GameObject weaponCamera;

    public Camera mainCamera;

    public Transform shootPointIdle;

    public float scopedFOV = 15f;
    private float normalFOV;

    private int isShooting;
    private int isReload;
    // private int isIdle = Animator.StringToHash("isIdle");

    private bool isScoped = false;

    // Start is called before the first frame update
    void Start()
    {
        //currentBullets = bulletsPerMag;
        anim = GetComponent<Animator>();
        isShooting = Animator.StringToHash("isShooting");
        isReload = Animator.StringToHash("isReload");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Aim();

        }
        if (!isScoped && Input.GetMouseButtonDown(1))
        {

        }

        /* if(Input.GetMouseButtonDown(1))
         {
             if(currentBullets == 0)
             {
                 Reload();
                 anim.SetBool("isReload", false);
                 currentBullets = bulletsPerMag;
                 totalbulletsleft -= bulletsPerMag;

             } else {
                 currentBullets--;
             }
         }*/
    }

    private void Aim()
    {
        isScoped = !isScoped;
        anim.SetBool(isShooting, isScoped);

        if (isScoped)
        {
            StartCoroutine(OnScoped());
        }
        else
        {
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

        if (isScoped)
        {
            isScoped = false;
            OnUnscoped();
            anim.SetBool(isReload, true);
            anim.SetBool(isShooting, false);
        }
        else
        {
            anim.SetBool(isReload, true);
            anim.SetBool(isShooting, false);
        }
    }
}
