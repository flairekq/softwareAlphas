using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Photon.Pun;

public class AimWeapon : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 0.5f;

    public Animator RigController; 

    /*
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
     void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }


    } */


    public Rig shootLayer;

    public Rig aimLayer;

    private PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        //right mouse click to aim
        if (Input.GetMouseButtonDown(1))
        {
            shootLayer.weight = shootLayer.weight == 0 ? 1 : 0; ;
            aimLayer.weight = shootLayer.weight == 0 ? 1 : 0;
        }

    }
    
}
