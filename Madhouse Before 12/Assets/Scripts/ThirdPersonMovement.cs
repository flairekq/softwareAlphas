using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class ThirdPersonMovement : MonoBehaviour
{
    // Start is called before the first frame update

  /* [SerializeField] private float moveSpeed;
   [SerializeField] private float walkSpeed;
   [SerializeField] private float runSpeed;
   [SerializeField] private BooleanSwitch isGrounded;
   [SerializeField] private float checkDist;
   [SerializeField] private LayerMask groundMask;
   [SerializeField] private float gravity;

    private float verticalVelocity;

    private float jumpForce = 3.5f;
   private Vector3 moveDirection;
   private Vector3 velocity;

   private bool isGround;

   private CharacterController controller;

   private void Start() {
       controller = GetComponent<CharacterController>();
   }

   private void Update() {
       Move();
   }

   private void Move() {

       isGround = Physics.CheckSphere(transform.position, checkDist, groundMask);

        if(isGround && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
       //z axis 
        float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
       Vector3 move = transform.right * x + transform.forward * z;
   
        controller.Move(move * walkSpeed * Time.deltaTime);
       
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        */

/*
   private CharacterController controller; 

   private Animator animator;

    private float speed = 0f;

    private float runSpeed = 3f;

    private float walkSpeed = 2f;
    private float verticalVelocity;
    private float gravity = 9.81f;
    private float jumpForce = 5f;

    private void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update() {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        Vector3 move = transform.right * x + transform.forward * z;

        if(controller.isGrounded) {
            verticalVelocity = -gravity * Time.deltaTime;


            //jump
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                verticalVelocity = jumpForce;
    
            } 

            if(move != Vector3.zero) 
            {
               
               //walking backwards animation
               if(Input.GetKeyDown(KeyCode.S))
                 {
                     WalkBackward();
                 } else {
                     WalkForward();
                 }
                 

                 if(Input.GetKeyDown(KeyCode.LeftAlt)) {
                     Run();
                 } else {
                     WalkForward();
                 }

            } else {
                Idle();
            }

        } else {
            verticalVelocity -= gravity * Time.deltaTime;
        } 

        move *= speed;
        
        controller.Move(move * speed * Time.deltaTime);
        

        controller.Move(moveVector  * Time.deltaTime);
    }


    private void Idle() 
    {
       animator.SetFloat("Speed", 0, 0.01f, Time.deltaTime);
    }

    private void WalkForward() 
    {
         speed = walkSpeed;
         animator.SetFloat("Speed", 0.33f, 0.01f, Time.deltaTime);
    }

    private void WalkBackward() 
    {
        speed = walkSpeed;
        //animator.SetFloat("Speed", 1f, 0.01f, Time.deltaTime);
    }

    private void Run() {
        speed = runSpeed;
         animator.SetFloat("Speed", 0.9f, 0.01f, Time.deltaTime);
    }
    */
    
    

private float moveSpeed;
public float rotateSpeed = 200f;

private float runSpeed = 1.5f;
private float walkSpeed = 0.5f;

private CharacterController controller;
private Rigidbody rb;

private Animator anim;

private Vector3 direction; 

private Vector3 forward;
private float verticalVelocity;
private float gravity = 9.81f;
private float jumpForce = 9f;

private bool front = true;
private bool left = false;
private bool right = false;

private bool facingBack = false;

private int forwardWalking = Animator.StringToHash("forwardWalking");
private int backwardsWalk = Animator.StringToHash("backwardsWalk");

private int isIdle = Animator.StringToHash("isIdle");

private int isRunning = Animator.StringToHash("isRunning");

private int backwardsRun = Animator.StringToHash("backwardsRun");

void Start() 
{
    rb = GetComponent<Rigidbody>();
    controller = GetComponent<CharacterController>();
    anim = GetComponent<Animator>();
}

void Update()
{
    Movement();
}

void Movement()
{
    float horizontalMove = Input.GetAxisRaw("Horizontal");
    float verticalMove = Input.GetAxisRaw("Vertical");

    Vector3 move = new Vector3(0,0, verticalMove);
    Vector3 move2 = new Vector3(horizontalMove, 0, 0);

    direction = new Vector3(horizontalMove, 0.0f, verticalMove);
    //vector to control jump 
    Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
    
    
    // if(direction != Vector3.zero) {

        if(move == Vector3.zero && move2 == Vector3.zero) {
        Idle();
    } 
        //controls rotation
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)) {
        transform.rotation = Quaternion.Slerp( transform.rotation, 
        Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            if(Input.GetKeyDown(KeyCode.A)) {
                front = true;
                left = false;
                right = false;
                facingBack = false;
            } 
            if(Input.GetKeyDown(KeyCode.A)) {
                front = false; 
                left = true;
                right = false;
                facingBack = false;
            } 
            if(Input.GetKeyDown(KeyCode.D)) {
                front = false; 
                left = false; 
                right = true; 
                facingBack = false;
            }
        } 

          /* if(Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.A))
           || (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.D)) ||
           (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.S)) || (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.S)))
            { */

        if(Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S)) {
            WalkBackwards();
            if(left) {
           transform.Rotate(Vector3.up, 90);
           left = false; 
           front = true;
            } else if(right) {
                transform.Rotate(Vector3.up, -90);
                right = false; 
                front = true;
            }
            WalkBackwards();

        }  if(!Input.GetKey(KeyCode.S))
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)  ||Input.GetKey(KeyCode.W))
            {
                 WalkForward();
             }
         }        
        
      
         if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S)) {
            Idle();
        } if(Input.GetKey(KeyCode.LeftShift)) {
            if(facingBack) {
                RunBackwards();
            } else {
                Run();
            }
        } 
       
     //  rb.MovePosition(transform.position  + moveSpeed * Time.deltaTime* direction);
        
   // }

    //jump
    if(controller.isGrounded) {
            verticalVelocity = -gravity * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                verticalVelocity = jumpForce;
               
            } 

    } else {
        verticalVelocity -= gravity * Time.deltaTime;
        
    }
    

    controller.Move(moveVector  * Time.deltaTime);
    move *= moveSpeed;
    move2 *= moveSpeed;
    controller.Move(move * moveSpeed * Time.deltaTime);
    controller.Move(move2 *moveSpeed * Time.deltaTime);

}

private void Idle() {
  /*  anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    anim.SetFloat("RunSpeed", 1.5f, 0, Time.deltaTime);*/

    anim.SetBool(backwardsWalk, false);
     anim.SetBool(forwardWalking, false);
     anim.SetBool(isIdle, true);
     anim.SetBool(isRunning, false);
     anim.SetBool(backwardsRun, false);
}

private void WalkForward() {
    //0-0.5, 0.5-1
    facingBack = false; 
    moveSpeed = walkSpeed;
   /* anim.SetFloat("Speed", 0.8f , 0f, Time.deltaTime);
    anim.SetFloat("RunSpeed", 0, 0, Time.deltaTime);*/
    anim.SetBool(forwardWalking, true);
    anim.SetBool(backwardsWalk, false);
    anim.SetBool(isIdle, false);
    anim.SetBool(isRunning, false);
    
}

private void WalkBackwards() {
    //1-1.5, 1.5-2
    facingBack = true;
   moveSpeed = walkSpeed;
   anim.SetBool(forwardWalking, false);
   anim.SetBool(backwardsWalk, true);
   anim.SetBool(isIdle, false);
   anim.SetBool(isRunning, false);
   anim.SetBool(backwardsRun, false);
   // anim.SetFloat("Speed", 1.8f, 0f, Time.deltaTime);
}

private void Jump() {
    //2-2.5, 2.5-3
    anim.SetFloat("Speed", 2.7f, 0f, Time.deltaTime);
}

private void Run() {
    facingBack = false;
    moveSpeed = runSpeed;
    anim.SetBool(isRunning, true);
    anim.SetBool(forwardWalking, false);
    anim.SetBool(backwardsWalk, false);
    anim.SetBool(isIdle, false);
}

private void RunBackwards() {
    facingBack = true;
    moveSpeed = runSpeed; 
    anim.SetBool(backwardsRun, true);
    anim.SetBool(backwardsWalk, false);
    
}

private void turnLeft() {
    anim.SetFloat("Speed", 2.7f, 0f, Time.deltaTime);
}





}

    





