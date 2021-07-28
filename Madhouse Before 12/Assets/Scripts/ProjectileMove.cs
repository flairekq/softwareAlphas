using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    private Rigidbody rb;

    private TrailRenderer tr;

    private MeshRenderer mesh;

    private PhotonView PV;

    private void Awake()
    {

        PV = GetComponent<PhotonView>();
    }
    // // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (speed != 0)
        // {
        transform.position += transform.forward * (speed * Time.deltaTime);
        // }

        // Invoke("Delay", 10f);

    }

    /*
     void OnCollisionEnter(Collision co)
     {
         if (!PV.IsMine)
         {
             return;
        }
         PV.RPC("deactivateBullet", RpcTarget.All);
     }
     */

    public void DeactivateBullet()
    {
        PV.RPC("RPC_HandleDeactivateBullet", RpcTarget.All);
    }


    [PunRPC]
    private void RPC_HandleDeactivateBullet()
    {
        if (rb != null && tr != null)
        {
            rb.isKinematic = true;
            tr.Clear();
            gameObject.SetActive(false);
        }
    }
}
