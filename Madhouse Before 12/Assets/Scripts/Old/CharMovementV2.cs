using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovementV2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    private Vector3 moveDirection;
    private Vector3 velocity;
    private CharacterController controller;
    private Animator animator;
    private bool isMoving = false;
    private bool isJumping = false;
    private float animationLength = 0f;
    private bool isAttacking = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        // press right mouse
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (animationLength <= 0)
            {
                Attack();
            }
        }

        if (animationLength > 0)
        {
            animationLength -= Time.deltaTime;
        }
        else if (isAttacking && animationLength <= 0)
        {
            DeactivateAttack();
        } else {}
    }

    private void Move()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (controller.isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                DeactivateAttack();
                isMoving = true;
                // walk
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                DeactivateAttack();
                isMoving = true;
                // run
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                isMoving = false;
                // idle
                Idle();
            }
            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                Jump();
            }
            else
            {
                isJumping = false;
            }
        }

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", isJumping);
    }

    private void DeactivateAttack()
    {
        isAttacking = false;
        animationLength = 0;
        animator.SetBool("isAttacking", isAttacking);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetBool("isMoving", isMoving);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetBool("isMoving", isMoving);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        animator.SetBool("isJumping", isJumping);
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);
        animationLength = 1f;
    }
}
