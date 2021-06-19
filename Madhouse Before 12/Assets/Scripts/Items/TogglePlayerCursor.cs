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
    private RifleManager rifleManager;
    private PhotonView PV;
    private CrosshairDetectItem crosshairDetectItem;
    private Reticle reticleScript;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        // crossHair = GameObject.FindGameObjectWithTag("CrossHairCanvas");
        movement = GetComponent<PlayerMotor>();
        mouseLook = GetComponent<FPSCamera>();
        playerController = GetComponent<MultiplayerController>();
        rifleManager = GetComponentInChildren<RifleManager>();
        crosshairDetectItem = crossHair.GetComponentInChildren<CrosshairDetectItem>();
        reticleScript = reticle.GetComponentInChildren<Reticle>();

        ChangeToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
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
        crosshairDetectItem.enabled = false;
        reticleScript.enabled = false;
        crossHair.SetActive(false);
        reticle.SetActive(false);
    }

    // this method is for power generator 
    public void ChangeToCursorZoomIn()
    {
        this.ChangeToCursor();
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
        rifleManager.enabled = true;
        crosshairDetectItem.enabled = true;
        reticleScript.enabled = true;
        crossHair.SetActive(true);
        reticle.SetActive(true);
    }

    // this method is for power generator 
    public void ChangeToPlayerZoomOut()
    {
        this.ChangeToPlayer();
        this.OnRenderer();
    }
}
