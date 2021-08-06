using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TogglePlayerCursor : MonoBehaviour
{
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject reticle;
    [SerializeField] private MeshRenderer[] gunRenderes;

    private PlayerMotor movement;
    private FPSCamera mouseLook;
    private MultiplayerController playerController;
    // private RifleManager rifleManager;
    private GunProjectile2 gunProjectile;
    private SpawnProjectile spawnProjectile;
    private Flashlight flashlight;
    private Inventory inventory;
    private EscMenu escMenu;
    private CrosshairDetectItem crosshairDetectItem;
    private PhotonView PV;
    private bool isInCursorMode = false;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            movement = GetComponent<PlayerMotor>();
            mouseLook = GetComponent<FPSCamera>();
            playerController = GetComponent<MultiplayerController>();
            // rifleManager = GetComponentInChildren<RifleManager>();
            gunProjectile = GetComponentInChildren<GunProjectile2>();
            spawnProjectile = GetComponentInChildren<SpawnProjectile>();
            inventory = GetComponent<Inventory>();
            flashlight = GetComponentInChildren<Flashlight>();
            escMenu = GetComponent<EscMenu>();
            crosshairDetectItem = crossHair.GetComponent<CrosshairDetectItem>();

            GameController.instance.AddPlayer(PV.ViewID);
            ChangeToPlayer();
        }
    }

    public void ChangeToCursor(bool isInventoryMode)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        movement.enabled = false;
        mouseLook.enabled = false;
        playerController.enabled = false;
        // rifleManager.enabled = false;
        gunProjectile.enabled = false;
        spawnProjectile.enabled = false;
        crossHair.SetActive(false);
        reticle.SetActive(false);
        crosshairDetectItem.enabled = false;
        isInCursorMode = true;

        if (!isInventoryMode)
        {
            inventory.enabled = false;
        }
    }

    // this method is for power generator 
    public void ChangeToCursorZoomIn()
    {
        this.ChangeToCursor(false);
        this.OffRenderer();
    }

    private void OnRenderer()
    {
        foreach (MeshRenderer r in gunRenderes)
        {
            r.enabled = true;
        }
    }

    private void OffRenderer()
    {
        foreach (MeshRenderer r in gunRenderes)
        {
            r.enabled = false;
        }
    }

    public void ChangeToPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movement.enabled = true;
        mouseLook.enabled = true;
        playerController.enabled = true;
        // rifleManager.enabled = true;
        gunProjectile.enabled = true;
        spawnProjectile.enabled = true;
        crosshairDetectItem.enabled = true;
        inventory.enabled = true;
        crossHair.SetActive(true);
        reticle.SetActive(true);
        isInCursorMode = false;
    }

    // this method is for power generator 
    public void ChangeToPlayerZoomOut()
    {
        this.ChangeToPlayer();
        this.OnRenderer();
    }

    public void GameOver()
    {
        if (PV.IsMine)
        {
            this.ChangeToCursor(false);
            // inventory.enabled = false;
            flashlight.enabled = false;
            escMenu.enabled = false;
        }
    }

    public bool IsInCursorMode()
    {
        return isInCursorMode;
    }
}
