using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed; 
    public float fireRate;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }

       // Invoke("Delay", 10f);
        
    }

    void OnCollisionEnter(Collision co)
    {
        // Debug.Log("bullet hit and destroyed");
        speed = 0;
        Destroy(gameObject);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }
}
