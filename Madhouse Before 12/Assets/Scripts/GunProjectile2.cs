using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GunProjectile : MonoBehaviour
{

    public GameObject bullet;

    public float shootForce;
    public float upwardForce; 

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;

    public int magazineSize, bulletsPerTap;

    public bool allowButtonHold;

    int bulletsLeft, bulletsShot; 

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;

    public Transform attackPoint; 

    public bool allowInvoke = true; 

    public float damage = 10f;
    public float range = 100f;
    public Transform shootPoint;
    //public CameraShake cameraShake;

    private AudioSource gunAudio;

    public GameObject muzzleFlashObject;
    public ParticleSystem muzzleFlash;

    private Animator anim;

    private bool aiming = false;

    // private int isShooting = Animator.StringToHash("isShooting");

    // private int isRecoil = Animator.StringToHash("isRecoil");
    private int isShooting;
    private int isRecoil;
    private int isIdle;

   // public Transform CameraPos;
    private PhotonView PV;
    [SerializeField] private CharacterCombat characterCombat;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true; 
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
        isShooting = Animator.StringToHash("isShooting");
        isRecoil = Animator.StringToHash("isRecoil");
        isIdle = Animator.StringToHash("isIdle");
    }

    // Update is called once per frame
    void Update()
    {
        // move gun to shooting position
        if (!PV.IsMine)
        {
            return;
        }

       if(Input.GetMouseButtonDown(0)) {
           if(!aiming)
           {
               Aim();
           }
                gunAudio.Play();
                Shoot();
              //  Recoil();
                 
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if (!aiming)
            {
                Aim();
            }
            else
            {
                aiming = false;
                Idle();
            }
        } 
        else if(aiming) 
        {
            
            Aim();
        }
        else if (!aiming)
        {
            
            Idle();
        }

        
 
    }

    void Aim()
    {
        aiming = true;
        anim.SetBool("isIdle", false);
        anim.SetBool(isRecoil, false);
        // anim.SetBool("isShooting", true);
        anim.SetBool(isShooting, true);
    }

    void Idle()
    {
        anim.SetBool(isRecoil, false);
        // anim.SetBool("isShooting", false);
        anim.SetBool(isShooting, false);
        anim.SetBool(isIdle, true);
    }

    void Recoil()
    {
        anim.SetBool(isRecoil, true);
        anim.SetBool(isShooting, false);
        anim.SetBool(isIdle, false);

    }

    void Shoot()
    {
       muzzleFlashObject.SetActive(true); 
      
    
           muzzleFlash.Clear();
            muzzleFlash.time = 0;
           muzzleFlash.Simulate(0f, true, true);
    
           muzzleFlash.Play();
           
       //ray through middle of screen 
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //check if ray hits something
    
        Vector3 targetPoint;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            targetPoint = hit.point;
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.Attacked(characterCombat);
            }
    } else {
        targetPoint = ray.GetPoint(75);
    }

    //calculate direction from attackPoint to targetPoint
    Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

    //calculate spread
    float x = Random.Range(-spread, spread);
    float y = Random.Range(-spread, spread);

    //Calculate new direction with spread 
    Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y,0);

    //Instantiate bullet/projectile 
    GameObject currentBullet = Instantiate( bullet, attackPoint.position, Quaternion.identity);
    currentBullet.transform.forward = directionWithSpread.normalized;

    //add force to bullet 
    currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
   
    }
}
