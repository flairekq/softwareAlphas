using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed; 
    public float fireRate;

    private Rigidbody rb;

    private TrailRenderer tr;

    private MeshRenderer mesh;
    // // Start is called before the first frame update
     void Start()
     {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        mesh = GetComponent<MeshRenderer> ();
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

    void OnCollisionEnter(Collision co)
    {
        // Debug.Log("bullet hit and destroyed");
       // speed = 0;
       // rb.velocity = Vector3.zero;
       // rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        tr.Clear();
       // mesh.enabled = false;
       // transform.rotation = Quaternion.identity;
         gameObject.SetActive(false);
    }
    private void Delay()
    {
       gameObject.SetActive(false);
    }
}
