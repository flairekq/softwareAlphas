using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisplayUIDoor : DisplayUI
{
    public GameObject behindDoorDisplay;
    public bool behindDoorDisplayInfo;
    public MeshCollider meshCollider;
    public MeshCollider meshCollider2;
    public BoxCollider boxCollider;
    public PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        boxCollider = GetComponent<BoxCollider>();

    }

    public override void FadeCanvas()
    {
        if (animator.GetBool("isOpeningFront"))
        {
            display.SetActive(false);
        }
        else
        {
            display.SetActive(displayInfo);
        }

        if (animator.GetBool("isOpeningBack"))
        {
            behindDoorDisplay.SetActive(false);
        }
        else
        {
            behindDoorDisplay.SetActive(behindDoorDisplayInfo);
        }
    }

    public override void SetDisplayInfo(bool val)
    {
        if (playerPos != Vector3.zero)
        {
            Vector3 doorRelative = this.transform.InverseTransformPoint(playerPos);
            if (meshCollider2 != null)
            {
                if (doorRelative.z < 0)
                {
                    displayInfo = val;
                    behindDoorDisplayInfo = false;
                }
                else
                {
                    behindDoorDisplayInfo = val;
                    displayInfo = false;
                }
            }
            else
            {
                if (doorRelative.z > 0)
                {
                    displayInfo = val;
                    behindDoorDisplayInfo = false;
                }
                else
                {
                    behindDoorDisplayInfo = val;
                    displayInfo = false;
                }
            }
        }
    }

    public override bool IsMouseOvering()
    {
        if (displayInfo || behindDoorDisplayInfo)
        {
            return true;
        }
        return false;
    }

    public override void OpenDoor(Vector3 pos)
    {
        base.OpenDoor(pos);

        Vector3 doorRelative = this.transform.InverseTransformPoint(playerPos);
        if ((doorRelative.z > 0 && meshCollider2 == null) || (doorRelative.z < 0 && meshCollider2 != null))
        {
            // animator.SetBool("isOpeningFront", true);
            PV.RPC("RPC_HandleDoor", RpcTarget.All, "isOpeningFront", true);

        }
        else
        {
            // animator.SetBool("isOpeningBack", true);
            PV.RPC("RPC_HandleDoor", RpcTarget.All, "isOpeningBack", true);
        }
        playerPos = Vector3.zero;
    }

    public void CloseDoor(string animationName)
    {
        PV.RPC("RPC_HandleDoor", RpcTarget.All, animationName, false);
    }

    [PunRPC]
    private void RPC_HandleDoor(string animationName, bool val)
    {
        animator.SetBool(animationName, val);
    }

}
