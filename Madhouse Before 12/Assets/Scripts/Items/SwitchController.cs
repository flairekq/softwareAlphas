using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchController : MonoBehaviour
{
    private Animator animator;
    private PhotonView PV;
    private bool isOn = false;

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
        // isOn = true;
        PV.RPC("RPC_HandleSwitch", RpcTarget.All, "isOn", true);
    }

    public void OffSwitch()
    {
        // isOn = false;
        PV.RPC("RPC_HandleSwitch", RpcTarget.All, "isOn", false);
    }

    public bool IsSwitchOn() {
        return isOn;
    }

    [PunRPC]
    private void RPC_HandleSwitch(string animationName, bool val)
    {
        isOn = val;
        animator.SetBool(animationName, val);
    }
}
