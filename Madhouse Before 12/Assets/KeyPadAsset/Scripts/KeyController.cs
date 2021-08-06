//Script written by Matthew Rukas - Volumetric Games || volumetricgames@gmail.com || www.volumetric-games.com
// Updated by softwareAlphas

using UnityEngine;
using System.Collections;
using Photon.Pun;

public class KeyController : MonoBehaviour
{
    private KeyPadController kpController;
    private GameObject activeChar;
    private PhotonView PV;
    // private KeyPadRay kpRay;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        kpController = this.gameObject.GetComponent<KeyPadController>();
        // kpRay = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<KeyPadRay>();
    }

    public void KeyPressNum(int keyNumber)
    {
        kpController.SingleBeep();
        // kpController.text.color = Color.black;

        if (kpController.codeText.characterLimit <= (kpController.characterLim - 1))
        {
            // kpController.codeText.characterLimit++;
            // kpController.codeText.text = kpController.codeText.text + keyNumber;
            PV.RPC("RPC_HandleKeyPressNum", RpcTarget.All, keyNumber);
        }
    }

    [PunRPC]
    private void RPC_HandleKeyPressNum(int keyNumber)
    {
        kpController.codeText.characterLimit++;
        kpController.codeText.text = kpController.codeText.text + keyNumber;
    }

    public void KeyPressEnt()
    {
        kpController.CheckCode();
    }

    public void KeyPressClr()
    {
        kpController.SingleBeep();
        kpController.codeText.characterLimit = 0;
        kpController.codeText.text = string.Empty;
    }

    public void KeyPressClose()
    {
        kpController.SingleBeep();
        // kpRay.EnablePlayer();
        // kpRay.DisableUI();
        if (activeChar != null)
        {
            GetComponentInChildren<CanvasInteract>().CanvasOff();
            activeChar.GetComponent<TogglePlayerCursor>().ChangeToPlayer();
        }
    }

    public void ChangeActiveCharacter(GameObject c)
    {
        activeChar = c;
    }
}
