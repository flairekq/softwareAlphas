using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform CameraPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraPos.transform.position;
        transform.rotation = CameraPos.transform.rotation;
    }
}
