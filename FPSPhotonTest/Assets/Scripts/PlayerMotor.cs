using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    // [SerializeField] private Camera cam;

    // public Transform playerCamera;
    private Camera cam;
    private Transform playerCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private Vector3 cameraRotation = Vector3.zero;
    private Rigidbody rb;
    private PhotonView PV;

    private float cameraPitch = 0.0f;

    // [SerializeField] private Transform rightArm;
    // [SerializeField] private Transform rightForeArm;
    // [SerializeField] private Transform rightHand;
    // [SerializeField] private Transform rightHandTarget;
    // [SerializeField] private Transform rightHandHint;
    // [SerializeField] private Transform leftArm;
    // [SerializeField] private Transform leftForeArm;
    // [SerializeField] private Transform leftHand;
    // [SerializeField] private Transform leftHandTarget;
    // [SerializeField] private Transform leftHandHint;

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

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(rightArm.position);
    //         stream.SendNext(rightForeArm.position);
    //         stream.SendNext(rightHand.position);
    //         stream.SendNext(rightHandTarget.position);
    //         stream.SendNext(rightHandHint.position);
    //         stream.SendNext(leftArm.position);
    //         stream.SendNext(leftForeArm.position);
    //         stream.SendNext(leftHand.position);
    //         stream.SendNext(leftHandTarget.position);
    //         stream.SendNext(leftHandHint.position);
    //     }
    //     else
    //     {
    //         rightArm.position = (Vector3)stream.ReceiveNext();
    //         rightForeArm.position = (Vector3)stream.ReceiveNext();
    //         rightHand.position = (Vector3)stream.ReceiveNext();
    //         rightHandTarget.position = (Vector3)stream.ReceiveNext();
    //         rightHandHint.position = (Vector3)stream.ReceiveNext();
    //         leftArm.position = (Vector3)stream.ReceiveNext();
    //         leftForeArm.position = (Vector3)stream.ReceiveNext();
    //         leftHand.position = (Vector3)stream.ReceiveNext();
    //         leftHandTarget.position = (Vector3)stream.ReceiveNext();
    //         leftHandHint.position = (Vector3)stream.ReceiveNext();
    //     }
    // }
}
