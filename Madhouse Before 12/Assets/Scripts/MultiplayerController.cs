using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

[RequireComponent(typeof(PlayerMotor))]
public class MultiplayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    // [SerializeField] WeaponItem[] weapons;
    // [SerializeField] Transform[] childrenTransforms;

    // private int weaponIndex;
    // private int previousWeaponIndex = -1;

    private PlayerMotor motor;
    private Animator anim;

    // private int forwardWalking = Animator.StringToHash("forwardWalking");
    // private int backwardsWalk = Animator.StringToHash("backwardsWalk");

    // private int isIdle = Animator.StringToHash("isIdle");

    // private int isRunning = Animator.StringToHash("isRunning");

    // private int backwardsRun = Animator.StringToHash("backwardsRun");

    // private int isWalkingRight = Animator.StringToHash("isWalkingRight");
    // private int startWalking = Animator.StringToHash("startWalking");
    // private int isWalkingLeft = Animator.StringToHash("isWalkingLeft");
    // private int isJumping = Animator.StringToHash("isJumping");
    private int forwardWalking;
    private int backwardsWalk;

    private int isIdle;

    private int isRunning;

    private int backwardsRun;

    private int isWalkingRight;
    private int startWalking;
    private int isWalkingLeft;
    private int isJumping;

    private CharacterController cc;

    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
         cc = GetComponent<CharacterController>();
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();

        if (PV.IsMine)
        {
            // EquipWeapon(0);
            forwardWalking = Animator.StringToHash("forwardWalking");
            backwardsWalk = Animator.StringToHash("backwardsWalk");

            isIdle = Animator.StringToHash("isIdle");

            isRunning = Animator.StringToHash("isRunning");

            backwardsRun = Animator.StringToHash("backwardsRun");

            isWalkingRight = Animator.StringToHash("isWalkingRight");
            startWalking = Animator.StringToHash("startWalking");
            isWalkingLeft = Animator.StringToHash("isWalkingLeft");
            isJumping = Animator.StringToHash("isJumping");
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);


        //camera 
        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        motor.RotateCamera(_cameraRotation);



        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
        {
            WalkBackwards();
        }
        if (!Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
            {
                WalkForward();
            }
            if (Input.GetKey(KeyCode.D))
            {
                WalkRight();
            }
            if (Input.GetKey(KeyCode.A))
            {
                WalkLeft();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(cc.isGrounded)
            {
                Jump();
            }
        }
        
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.Space))
        {
            Idle();
        }


    }

    private void Idle()
    {

        anim.SetBool(backwardsWalk, false);
        anim.SetBool(forwardWalking, false);
        anim.SetBool(isWalkingRight, false);
        // anim.SetBool("startWalking", false);
        // anim.SetBool("isWalkingLeft", false);
        anim.SetBool(startWalking, false);
        anim.SetBool(isWalkingLeft, false);
        anim.SetBool(isIdle, true);
        // anim.SetBool("isJumping", false);
        anim.SetBool(isJumping, false);
    }

    private void StartWalking()
    {
        // anim.SetBool("startWalking", true);
        anim.SetBool(startWalking, false);
        anim.SetBool(isIdle, false);
    }

    private void WalkForward()
    {
        anim.SetBool(forwardWalking, true);
        anim.SetBool(backwardsWalk, false);
        anim.SetBool(isIdle, false);
    }

    private void WalkBackwards()
    {
        anim.SetBool(forwardWalking, false);
        anim.SetBool(backwardsWalk, true);
        anim.SetBool(isIdle, false);

    }

    private void WalkRight()
    {
        anim.SetBool(isIdle, false);
        anim.SetBool(isWalkingRight, true);
    }

    private void WalkLeft()
    {
        anim.SetBool(isIdle, false);
        // anim.SetBool("isWalkingLeft", true);
        anim.SetBool(isWalkingLeft, true);
    }

    private void Jump()
    {
        anim.SetBool(isIdle, false);
        // anim.SetBool("isJumping", true);
        anim.SetBool(isJumping, true);
    }


    // void EquipWeapon(int _index)
    // {
    //     if (_index == previousWeaponIndex)
    //     {
    //         return;
    //     }

    //     weaponIndex = _index;
    //     weapons[weaponIndex].itemGameObject.SetActive(true);

    //     if (previousWeaponIndex != -1)
    //     {
    //         weapons[previousWeaponIndex].itemGameObject.SetActive(false);
    //     }

    //     previousWeaponIndex = weaponIndex;

    //     if (PV.IsMine)
    //     {
    //         Hashtable hash = new Hashtable();
    //         hash.Add("weaponIndex", weaponIndex);
    //         PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    //     }

    // }


    // public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    // {
    //     if (!PV.IsMine && targetPlayer == PV.Owner)
    //     {
    //         EquipWeapon((int)changedProps["weaponIndex"]);
    //     }
    // }


    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         for (int i = 0; i < childrenTransforms.Length; i++)
    //         {
    //             if (childrenTransforms[i] != null)
    //             {
    //                 stream.SendNext(childrenTransforms[i].localPosition);
    //                 stream.SendNext(childrenTransforms[i].localRotation);
    //                 stream.SendNext(childrenTransforms[i].localScale);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         for (int i = 0; i < childrenTransforms.Length; i++)
    //         {
    //             if (childrenTransforms[i] != null)
    //             {
    //                 childrenTransforms[i].localPosition = (Vector3)stream.ReceiveNext();
    //                 childrenTransforms[i].localRotation = (Quaternion)stream.ReceiveNext();
    //                 childrenTransforms[i].localScale = (Vector3)stream.ReceiveNext();
    //             }
    //         }
    //     }
    // }

}