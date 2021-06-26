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

    // private int isShooting = Animator.StringToHash("isShooting");

    // private int isRecoil = Animator.StringToHash("isRecoil");
    private int isShooting;
    private int isRecoil;
    private int isIdle;
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
        isShooting = Animator.StringToHash("isShooting");
        isRecoil = Animator.StringToHash("isRecoil");
        isIdle = Animator.StringToHash("isIdle");
    }

    void Update()
    {

        // move gun to shooting position
        if (!PV.IsMine)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            Recoil();

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
        else if (aiming)
        {
            Aim();
        }
        else if (!aiming)
        {
            Idle();
        }

        /*
        if(aiming)
        {
            if(Input.GetMouseButtonDown(0)) {
                Shoot();
                Recoil();
            } if(Input.GetMouseButtonDown(1))
            {
                aiming = false;
               Idle();
            } else {
                Aim();
            }
        } if(!aiming) {
            if(Input.GetMouseButtonDown(0))
            {
                Shoot();
                Recoil();
            } if(Input.GetMouseButtonDown(1))
            {
                Aim(); 
            } else {
                Idle();
            }
        }*/
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
        muzzleFlash.Play();
        /*  anim.SetBool("isShooting", false);
          anim.SetBool("isIdle", false); 
          anim.SetBool(isRecoil, true);
          */
        RaycastHit hit;
        currentBullets--;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            // Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

}
