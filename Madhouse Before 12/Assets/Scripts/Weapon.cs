using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{   
    public float range = 100f;
    public int bulletsPerMag = 30;
    public int bulletsLeft = 200; //total bullets we have 
    public int currentBullets; // current bullets in our magazine

    public float fireRate = 0.1f;
    private float fireTimer;

    public Transform shootPoint;

    private Animator anim;

    public GameObject scopeOverlay;

    public GameObject weaponCamera;

    private int isShooting = Animator.StringToHash("isShooting");
   // private int isIdle = Animator.StringToHash("isIdle");

    private bool isScoped= false;

    // Start is called before the first frame update
    void Start()
    {
        currentBullets = bulletsPerMag;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Aim();
            
        }

        if(fireTimer < fireRate) {
            fireTimer += Time.deltaTime;
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
        
        /*
        if(fireTimer < fireRate) {
            return;
        }
        */

      /*  RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, 
        out hit, range))
        {
            Debug.Log(hit.transform.name + "found!");
        }

        currentBullets--;
        fireTimer = 0.0f; //Reset fire timer
        */
    }

    void OnUnscoped() 
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
    }

    IEnumerator OnScoped() 
    {
        yield return new WaitForSeconds(0.15f);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
    }
}
