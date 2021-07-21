using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRigidbody : MonoBehaviour
{
    private Rigidbody _rigidbody;
 
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
 
    private void OnEnable()
    {
        _rigidbody.WakeUp();
    }
 
    private void OnDisable()
    {
        _rigidbody.Sleep();
    }
}
