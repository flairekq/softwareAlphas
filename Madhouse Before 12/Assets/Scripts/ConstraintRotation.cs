using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConstraintRotation : MonoBehaviour
{
    // Start is called before the first frame update 

    private float rotationx;
    private float rotationy;

    public float distance;

    public Transform mainCamera;

    private PhotonView PV;
    void Awake()
    {
        PV = gameObject.transform.parent.GetComponent<PhotonView>();
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

        transform.position = mainCamera.transform.position + mainCamera.transform.forward
         * distance;

        rotationx = Mathf.Clamp(mainCamera.transform.rotation.x, -20f, 20f);
        rotationy = Mathf.Clamp(mainCamera.transform.rotation.y, -20f, 20f);

        transform.rotation = Quaternion.Euler(rotationx, rotationy, mainCamera.transform.rotation.z);
    }
}