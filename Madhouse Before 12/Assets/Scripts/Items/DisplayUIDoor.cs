using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisplayUIDoor : DisplayUI
{
    public string roomName;
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

    public override string OpenDoor(Vector3 pos, Inventory inventory)
    {
        base.OpenDoor(pos, inventory);

        if (roomName.Equals("dayroom") && !EnvironmentManager.instance.isDayroomUnlocked)
        {
            return "You need to enter the correct code";
        }

        if (roomName.Equals("classroom")
            && !EnvironmentManager.instance.isClassroomUnlocked)
        {
            if (inventory.isItemPicked("Classroom Key"))
            {
                EnvironmentManager.instance.ToggleLockUnlockDoor("classroom", true);
            }
            else
            {
                return "You need a key";
            }
        }

        if (roomName.Equals("basement")
            && !EnvironmentManager.instance.isBasementMainUnlocked)
        {
            if (inventory.isItemPicked("Basement Key"))
            {
                EnvironmentManager.instance.ToggleLockUnlockDoor("basement", true);
            }
            else
            {
                return "You need a key";
            }
        }

        Vector3 doorRelative = this.transform.InverseTransformPoint(playerPos);
        if ((doorRelative.z > 0 && meshCollider2 == null) || (doorRelative.z < 0 && meshCollider2 != null))
        {
            PV.RPC("RPC_HandleDoor", RpcTarget.All, "isOpeningFront", true);
        }
        else
        {
            PV.RPC("RPC_HandleDoor", RpcTarget.All, "isOpeningBack", true);
        }
        playerPos = Vector3.zero;
        return "successful";
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
