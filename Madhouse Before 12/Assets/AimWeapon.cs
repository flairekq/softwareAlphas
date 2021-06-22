using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimWeapon : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 0.5f;

    public Rig shootLayer;

    public Rig aimLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //right mouse click to aim
        if(Input.GetMouseButtonDown(1))
        {
            shootLayer.weight = shootLayer.weight == 0 ? 1 : 0;;
            aimLayer.weight = shootLayer.weight == 0? 1 : 0;
        } 
            
        
        
    }
}
