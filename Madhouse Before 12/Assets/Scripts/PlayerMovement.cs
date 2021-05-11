using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 8f;
    public float gravity = -9.81f;
    // public float jumpHeight = 0.0005f;
    public float jumpHeight = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpRememberTime = 0.2f;
    float jumpRememberTimer;

    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        jumpRememberTimer -= Time.deltaTime;

        // slam into the ground
        if (controller.isGrounded && velocity.y < 0) {
            // hit ground
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // transform.right takes the direction that the player is facing and goes to the right
        // transform.forward also takes the direction that the player is facing and goes forward
        Vector3 move = transform.right * x + transform.forward * z;

        // multiply by Time.deltaTime to be frame rate independent
        controller.Move(move * speed * Time.deltaTime);

        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     jumpRememberTimer = jumpRememberTime;
        // }

        // if (jumpRememberTimer > 0f && controller.isGrounded) {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            // velocity.y = jumpHeight;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
