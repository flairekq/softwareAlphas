using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Projector : MonoBehaviour
{
    public static Projector instance;
    [SerializeField] Text displayText;
    private PhotonView PV;

    void Awake()
    {
        instance = this;
        instance.PV = GetComponent<PhotonView>();
    }

    public void ToggleProjectorDisplay()
    {
        Projector.instance.PV.RPC("RPC_HandleProjectorDisplay", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_HandleProjectorDisplay()
    {
        Projector.instance.displayText.enabled = !displayText.enabled;
    }

    public void OffProjector()
    {
        Projector.instance.PV.RPC("RPC_HandleProjectorDisplayOff", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_HandleProjectorDisplayOff()
    {
        Projector.instance.displayText.enabled = false;
    }
}
