using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableObject : MonoBehaviour
{
    // Start is called before the first frame update

    public abstract void OnHit(RaycastHit hit);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
