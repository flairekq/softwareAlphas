using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerCursor : MonoBehaviour
{
    public GameObject crossHair;

    private PlayerMovement movement;
    private MouseLook mouseLook;
    private PlayerController playerController;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        mouseLook = GetComponentInChildren<MouseLook>();
        playerController = GetComponent<PlayerController>();
    }

    public void ChangeToCursor()
    {
        // Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        movement.enabled = false;
        mouseLook.enabled = false;
        playerController.enabled = false;
        crossHair.SetActive(false);

    }

    public void ChangeToPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movement.enabled = true;
        mouseLook.enabled = true;
        playerController.enabled = true;
        crossHair.SetActive(true);
    }
}
