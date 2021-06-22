using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    private Animator anim;

    private bool aiming = false;

    private int isShooting = Animator.StringToHash("isShooting");

    private int isRecoil = Animator.StringToHash("isRecoil");
    // private int isIdle = Animator.StringToHash("isIdle");
    private PhotonView PV;

    // public Transform CameraPos;

    // public Transform CharacterPos;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        currentBullets = bulletspermag;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        // move gun to shooting position
        if (!PV.IsMine)
        {
            return;
        }

        //Shooting 
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
           // Recoil();
            
        } if(Input.GetMouseButtonUp(0)) {
            Idle();
        }
       
    }

    void Aim()
    {
        aiming = true;
        anim.SetBool("isShooting", true);
        anim.SetBool(isRecoil, false);
        anim.SetBool("isIdle", false);
    }

    void Idle()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool(isRecoil, false);
        anim.SetBool("isShooting", false);
    }

    void Recoil()
    {
        anim.SetBool(isRecoil, true);
        anim.SetBool("isIdle", false);

    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        currentBullets--;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

}
