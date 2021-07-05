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
    public int isOpeningFrontId = 0;
    public int isOpeningBackId = 0;
    // [SerializeField] Renderer m_Renderer;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        isOpeningFrontId = Animator.StringToHash("isOpeningFront");
        isOpeningBackId = Animator.StringToHash("isOpeningBack");
        // m_Renderer = GetComponent<Renderer>();
        // Debug.Log(isOpeningFrontId);
    }

    public override void FadeCanvas()
    {
        // // if (animator.GetBool("isOpeningFront"))
        // if (isOpeningFrontId != 0 && isOpeningBackId != 0)
        // {
        //     Debug.Log(isOpeningFrontId);
        //     Debug.Log(animator);
        // Debug.Log(animator.GetBool(isOpeningFrontId));

        // if (m_Renderer.isVisible)
        // {
        // animator.enabled = true;
        if (animator.GetBool(isOpeningFrontId) && display.activeSelf)
        {
            display.SetActive(false);
        }
        else if (!animator.GetBool(isOpeningFrontId))
        {
            if (displayInfo && !display.activeSelf)
            {
                display.SetActive(true);
            }
            else if (!displayInfo && display.activeSelf)
            {
                display.SetActive(false);
            }
            // display.SetActive(displayInfo);
        }

        // if (animator.GetBool("isOpeningBack"))
        if (animator.GetBool(isOpeningBackId) && behindDoorDisplay.activeSelf)
        {
            behindDoorDisplay.SetActive(false);
        }
        else if (!animator.GetBool(isOpeningBackId))
        {
            // behindDoorDisplay.SetActive(behindDoorDisplayInfo);
            if (behindDoorDisplayInfo && !behindDoorDisplay.activeSelf)
            {
                behindDoorDisplay.SetActive(true);
            }
            else if (!behindDoorDisplayInfo && behindDoorDisplay.activeSelf)
            {
                behindDoorDisplay.SetActive(false);
            }
        }
        // } else {
        //     animator.enabled = false;
        // }

        // }

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
            // PV.RPC("RPC_HandleDoor", RpcTarget.All, "isOpeningFront", true);
            PV.RPC("RPC_HandleDoor", RpcTarget.All, isOpeningFrontId, true);
        }
        else
        {
            // PV.RPC("RPC_HandleDoor", RpcTarget.All, "isOpeningBack", true);
            PV.RPC("RPC_HandleDoor", RpcTarget.All, isOpeningBackId, true);
        }
        playerPos = Vector3.zero;
        return "successful";
    }

    // public void CloseDoor(string animationName)
    public void CloseDoor(int animationId)
    {
        PV.RPC("RPC_HandleDoor", RpcTarget.All, animationId, false);
    }

    [PunRPC]
    private void RPC_HandleDoor(int animationId, bool val)
    {
        animator.SetBool(animationId, val);
    }

}
