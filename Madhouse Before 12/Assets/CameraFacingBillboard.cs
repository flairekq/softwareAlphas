using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{

    private Transform mainCameraTransform;
    // Update is called once per frame

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.rotation 
        * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
        
    }
}
