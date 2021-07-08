using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// [RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Camera cam;
    private Transform playerCamera;
    private Vector3 velocity = Vector3.zero;

    private float gravity = 9.81f;
    private float jumpForce = 20f;
    private float verticalVelocity;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    // private Rigidbody rb;
    private CharacterController cc;
    private PhotonView PV;
    private Animator anim;

    // private float cameraPitch = 0.0f;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

    }

    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        anim = GetComponent<Animator>();
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
    // void FixedUpdate()
    void Update()
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

            // if (cc.isGrounded && verticalVelocity < 0)
            // {
            //     verticalVelocity = -2f;
            // }
            // else
            // {
            //     verticalVelocity -= gravity * Time.fixedDeltaTime;
            // }

            if (cc.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;
               Debug.Log("isgrounded");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("jump");
                  //  verticalVelocity = jumpForce;
                }

            } else {
               // Debug.Log("notGrounded");
               verticalVelocity -= gravity * Time.deltaTime;
            }
          

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);

        cc.Move(velocity * Time.deltaTime);
        cc.Move(moveVector * Time.deltaTime);

        // rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

    }

    void PerformRotation()
    {
        // rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {

            cam.transform.Rotate(-cameraRotation);
        }
    }

}
