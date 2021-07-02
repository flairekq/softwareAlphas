using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RifleManager2 : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform CameraPos;

    private float rotx;
    private float roty;

    private PhotonView PV;

    void Awake()
    {
        PV = gameObject.transform.parent.transform.parent.GetComponent<PhotonView>();
    }
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
         if (!PV.IsMine)
        {
            return;
        }

        transform.rotation = CameraPos.transform.rotation;
        
    }
}
