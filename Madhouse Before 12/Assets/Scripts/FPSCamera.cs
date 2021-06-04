using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform playerCamera;
    [SerializeField] float mouseSensitivity = 3.5f;

    [SerializeField] bool lockCursor = true;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private float cameraPitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(lockCursor) {
            Cursor.lockState =CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
    }

    void UpdateMouseLook() {
        
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -60.0f, 60.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);


    }
}

