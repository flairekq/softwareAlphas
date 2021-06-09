using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Camera cam;
    private Transform playerCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private Vector3 cameraRotation = Vector3.zero;
    private Rigidbody rb;
    private PhotonView PV;

    // private float cameraPitch = 0.0f;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        playerCamera = cam.transform;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }
    void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }

        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {

            cam.transform.Rotate(-cameraRotation);
        }
    }

}
