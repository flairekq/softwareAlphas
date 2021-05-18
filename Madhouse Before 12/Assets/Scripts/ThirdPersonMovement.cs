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

   private CharacterController controller; 

   private Animator animator;

    private float speed = 0f;

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
                Walk();

            } else {
                Idle();
            }

        } else {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        controller.Move(move * speed * Time.deltaTime);
        

        controller.Move(moveVector  * Time.deltaTime);
    }


    private void Idle() 
    {
       animator.SetFloat("Speed", 0, 0.01f, Time.deltaTime);
    }

    private void Walk() 
    {
         speed = walkSpeed;
         animator.SetFloat("Speed", 1f, 0.01f, Time.deltaTime);
    }

}

    





