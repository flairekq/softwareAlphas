using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class MultiplayerController : MonoBehaviour
{
   [SerializeField] private float speed = 5f;
   [SerializeField] private float lookSensitivity = 3f;

   private PlayerMotor motor; 
   private Animator anim;

private int forwardWalking = Animator.StringToHash("forwardWalking");
private int backwardsWalk = Animator.StringToHash("backwardsWalk");

private int isIdle = Animator.StringToHash("isIdle");

private int isRunning = Animator.StringToHash("isRunning");

private int backwardsRun = Animator.StringToHash("backwardsRun");


   void Start()
   {
       motor = GetComponent<PlayerMotor>();
       anim = GetComponentInChildren<Animator>();

   }

   void Update()
   {
       float _xMov = Input.GetAxisRaw("Horizontal");
       float _zMov = Input.GetAxisRaw("Vertical");

       Vector3 _movHorizontal = transform.right * _xMov;
       Vector3 _movVertical = transform.forward * _zMov;

       Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

       motor.Move(_velocity);

       float _yRot = Input.GetAxisRaw("Mouse X");
       Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

       motor.Rotate(_rotation);
        
        //camera 
        float _xRot = Input.GetAxisRaw("Mouse Y");
       Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

       motor.RotateCamera(_cameraRotation);

       

       if(Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S)) {
            WalkBackwards();
       } if(!Input.GetKey(KeyCode.S))
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)  ||Input.GetKey(KeyCode.W))
            {
                 WalkForward();
             }
         } else {
             Idle();
         }   
         

   }
   private void Idle() {

    anim.SetBool(backwardsWalk, false);
     anim.SetBool(forwardWalking, false);
     anim.SetBool(isIdle, true);
}

private void WalkForward() {
    anim.SetBool(forwardWalking, true);
    anim.SetBool(backwardsWalk, false);
    anim.SetBool(isIdle, false);
}

private void WalkBackwards() {
   anim.SetBool(forwardWalking, false);
   anim.SetBool(backwardsWalk, true);
   anim.SetBool(isIdle, false);
   
}

}