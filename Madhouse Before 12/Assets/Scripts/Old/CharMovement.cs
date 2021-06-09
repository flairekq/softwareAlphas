using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller; 

    private float speed = 6f;
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
