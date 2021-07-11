using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunCamera : MonoBehaviour
{
    public Transform playerCamera;
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        
        transform.rotation = playerCamera.rotation;
    }

}