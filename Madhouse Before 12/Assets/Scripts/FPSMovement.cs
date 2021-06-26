using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed;
    private float runSpeed = 1.5f;
    private float walkSpeed = 5f;

    private CharacterController controller;
    private Rigidbody rb;

    private Animator anim;

    private Vector3 direction;

    private Vector3 forward;
    private float verticalVelocity;
    private float gravity = 9.81f;
    private float jumpForce = 14f;
    private bool facingBack = false;

    private int forwardWalking = Animator.StringToHash("forwardWalking");
    private int backwardsWalk = Animator.StringToHash("backwardsWalk");

    private int isIdle = Animator.StringToHash("isIdle");

    private int isRunning = Animator.StringToHash("isRunning");

    private int backwardsRun = Animator.StringToHash("backwardsRun");

    private int isWalkingRight = Animator.StringToHash("isWalkingRight");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }

        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
        controller.Move(moveVector * Time.deltaTime);

        if (move == Vector3.zero && move == Vector3.zero)
        {
            Idle();
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
        {
            WalkBackwards();
        }
        if (!Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                WalkForward();
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S))
        {
            Idle();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (facingBack)
            {
                RunBackwards();
            }
            else
            {
                Run();
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }



        /*float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(0,0, verticalMove);
        Vector3 move2 = new Vector3(horizontalMove, 0, 0);

        direction = new Vector3(horizontalMove, 0.0f, verticalMove);
        //vector to control jump 
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);

            if(move == Vector3.zero && move2 == Vector3.zero) {
            Idle();
        } 

            if(Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S)) {
                WalkBackwards();
            }  if(!Input.GetKey(KeyCode.S))
            {
                if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) {
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
        */

    }

    private void Idle()
    {
        /*  anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
          anim.SetFloat("RunSpeed", 1.5f, 0, Time.deltaTime);*/

        anim.SetBool(backwardsWalk, false);
        anim.SetBool(forwardWalking, false);
        anim.SetBool(isIdle, true);
        anim.SetBool(isRunning, false);
        anim.SetBool(backwardsRun, false);
        anim.SetBool(isWalkingRight, false);
        anim.SetBool("isShooting", false);
    }

    private void WalkForward()
    {
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

    private void WalkRight()
    {
        moveSpeed = walkSpeed;
        anim.SetBool(isIdle, false);
        anim.SetBool(isWalkingRight, true);
    }

    private void WalkLeft()
    {
        moveSpeed = walkSpeed;
    }

    private void WalkBackwards()
    {
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

    private void Jump()
    {
        //2-2.5, 2.5-3
        anim.SetFloat("Speed", 2.7f, 0f, Time.deltaTime);
    }

    private void Run()
    {
        facingBack = false;
        moveSpeed = runSpeed;
        anim.SetBool(isRunning, true);
        anim.SetBool(forwardWalking, false);
        anim.SetBool(backwardsWalk, false);
        anim.SetBool(isIdle, false);
    }

    private void RunBackwards()
    {
        facingBack = true;
        moveSpeed = runSpeed;
        anim.SetBool(backwardsRun, true);
        anim.SetBool(backwardsWalk, false);

    }

    private void Shoot()
    {
        anim.SetBool("isShooting", true);
        anim.SetBool(isIdle, false);
    }

    private void turnLeft()
    {
        anim.SetFloat("Speed", 2.7f, 0f, Time.deltaTime);
    }
}
