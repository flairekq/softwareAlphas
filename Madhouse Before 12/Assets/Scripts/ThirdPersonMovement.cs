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

    private float speed = 4f;
    private float verticalVelocity;
    private float gravity = 9.81f;
    private float jumpForce = 3.5f;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if(controller.isGrounded) {
            verticalVelocity = -gravity * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space)) {
                verticalVelocity = jumpForce;
            }   
        } else {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);

        // transform.right takes the direction that the player is facing and goes to the right
        // transform.forward also takes the direction that the player is facing and goes forward
        Vector3 move = transform.right * x + transform.forward * z;
        // multiply by Time.deltaTime to be frame rate independent
        controller.Move(move * speed * Time.deltaTime);


        controller.Move(moveVector  * Time.deltaTime);
    }

}

    





