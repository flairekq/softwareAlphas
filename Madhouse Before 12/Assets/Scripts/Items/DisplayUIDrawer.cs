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
        animator = GetComponent<Animator>();
        isTopOpenId = Animator.StringToHash("isTopOpen");
        isBtmOpenId = Animator.StringToHash("isBtmOpen");
    }

    public void ToggleDrawer(int animationId)
    {
        PV.RPC("RPC_HandleDrawer", RpcTarget.All, animationId);
    }

    [PunRPC]
    private void RPC_HandleDrawer(int animationId)
    {
        animator.SetBool(animationId, !animator.GetBool(animationId));
    }
}
