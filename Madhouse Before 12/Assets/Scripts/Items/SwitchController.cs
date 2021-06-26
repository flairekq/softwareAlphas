using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchController : MonoBehaviour
{
    private Animator animator;
    private PhotonView PV;
    private bool isOn = false;
    int isOnId = 0;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        isOnId = Animator.StringToHash("isOn");
    }

    public void ToggleSwitch()
    {
        // if (animator.GetBool("isOn"))
        if (animator.GetBool(isOnId))
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
        // isOn = true;
        // PV.RPC("RPC_HandleSwitch", RpcTarget.All, "isOn", true);
        PV.RPC("RPC_HandleSwitch", RpcTarget.All, isOnId, true);
    }

    public void OffSwitch()
    {
        // isOn = false;
        // PV.RPC("RPC_HandleSwitch", RpcTarget.All, "isOn", false);
        PV.RPC("RPC_HandleSwitch", RpcTarget.All, isOnId, false);
    }

    public bool IsSwitchOn()
    {
        return isOn;
    }

    [PunRPC]
    private void RPC_HandleSwitch(int animationId, bool val)
    {
        isOn = val;
        animator.SetBool(animationId, val);
    }
}
