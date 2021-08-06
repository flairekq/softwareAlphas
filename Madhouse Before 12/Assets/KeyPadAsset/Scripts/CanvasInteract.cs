//Script written by Matthew Rukas - Volumetric Games || volumetricgames@gmail.com || www.volumetric-games.com
// Updated by softwareAlphas

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class CanvasInteract : MonoBehaviour
{
    [SerializeField] private Canvas keyPadCanvas;
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void CanvasOn()
    {
        // keyPadCanvas.enabled = true;
        PV.RPC("RPC_HandleKeyPadCanvas", RpcTarget.All, true);
    }

    public void CanvasOff()
    {
        // keyPadCanvas.enabled = false;
        PV.RPC("RPC_HandleKeyPadCanvas", RpcTarget.All, false);
    }

    [PunRPC]
    private void RPC_HandleKeyPadCanvas(bool isEnabled)
    {
        keyPadCanvas.enabled = isEnabled;
    }

    public bool IsCanvasOn()
    {
        return keyPadCanvas.enabled;
    }
}
