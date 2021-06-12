using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchController : MonoBehaviour
{
    private Animator animator;
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleSwitch()
    {
        if (animator.GetBool("isOn"))
        {
            OffSwitch();
        }
        else
        {
            OnSwitch();
        }
    }

    private void OnSwitch()
    {
        PV.RPC("RPC_HandleSwitch", RpcTarget.All, "isOn", true);
    }

    public void OffSwitch()
    {
        PV.RPC("RPC_HandleSwitch", RpcTarget.All, "isOn", false);
    }

    [PunRPC]
    private void RPC_HandleSwitch(string animationName, bool val)
    {
        animator.SetBool(animationName, val);
    }
}
