using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisplayUIDrawer : DisplayUI
{
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void ToggleDrawer(string animationName)
    {
        PV.RPC("RPC_HandleDrawer", RpcTarget.All, animationName);
    }

    [PunRPC]
    private void RPC_HandleDrawer(string animationName)
    {
        animator.SetBool(animationName, !animator.GetBool(animationName));
    }
}
