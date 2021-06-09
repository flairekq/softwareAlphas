using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerCursor : MonoBehaviour
{
    [SerializeField] private GameObject crossHair;

    private PlayerMotor movement;
    private FPSCamera mouseLook;
    private MultiplayerController playerController;
    private RifleManager rifleManager;

    void Start()
    {
        // crossHair = GameObject.FindGameObjectWithTag("CrossHairCanvas");
        movement = GetComponent<PlayerMotor>();
        mouseLook = GetComponent<FPSCamera>();
        playerController = GetComponent<MultiplayerController>();
        rifleManager = GetComponentInChildren<RifleManager>();
    }

    public void ChangeToCursor()
    {
        // Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        movement.enabled = false;
        mouseLook.enabled = false;
        playerController.enabled = false;
        rifleManager.enabled = false;
        crossHair.SetActive(false);

    }

    public void ChangeToPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movement.enabled = true;
        mouseLook.enabled = true;
        playerController.enabled = true;
        rifleManager.enabled = true;
        crossHair.SetActive(true);
    }
}
