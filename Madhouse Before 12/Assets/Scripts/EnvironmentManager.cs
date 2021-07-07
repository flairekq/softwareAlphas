using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnvironmentManager : MonoBehaviour
{
    #region Singleton
    public static EnvironmentManager instance;
    public PhotonView PV;

    void Awake()
    {
        // PV = GetComponent<PhotonView>();
        instance = this;
        instance.PV = GetComponent<PhotonView>();
    }

    #endregion

    // public GameObject[] basementRooms;
    public PositionRange[] basementPositionRange;
    // public GameObject[] firstFloorRooms;
    public PositionRange[] firstFloorPositionRange;
    // public GameObject[] secondFloorRooms;
    public PositionRange[] secondFloorPositionRange;

    public PositionRange[] classroomPositionRange;
    public PositionRange[] bedroomPositionRange;
    public PositionRange[] dayroomPositionRange;
    public bool isClearBasement = false;
    public Transform powerGenerator;
    public bool isPowerOn = false;
    public bool isDayroomUnlocked = false;
    public bool isClassroomUnlocked = false;
    public bool isBasementMainUnlocked = false;
    public bool isMainDoorUnlocked = false;
    public bool isProjectorOn = false;

    public void TogglePower(bool val)
    {
        EnvironmentManager.instance.PV.RPC("RPC_HandlePowerOnOff", RpcTarget.All, val);
    }

    [PunRPC]
    private void RPC_HandlePowerOnOff(bool val)
    {
        EnvironmentManager.instance.isPowerOn = val;
    }

    public void ToggleProjector(bool val)
    {
        EnvironmentManager.instance.PV.RPC("RPC_HandleProjectorOnOff", RpcTarget.All, val);
    }

    [PunRPC]
    private void RPC_HandleProjectorOnOff(bool val)
    {
        EnvironmentManager.instance.isProjectorOn = val;
    }

    public void ToggleLockUnlockDoor(string name, bool val)
    {
        EnvironmentManager.instance.PV.RPC("RPC_ToggleLockUnlockDoor", RpcTarget.All, name, val);
    }

    [PunRPC]
    private void RPC_ToggleLockUnlockDoor(string name, bool isLocked)
    {
        switch (name)
        {
            case "dayroom":
                EnvironmentManager.instance.isDayroomUnlocked = isLocked;
                break;
            case "classroom":
                EnvironmentManager.instance.isClassroomUnlocked = isLocked;
                break;
            case "basement":
                EnvironmentManager.instance.isBasementMainUnlocked = isLocked;
                break;
            case "main":
                EnvironmentManager.instance.isMainDoorUnlocked = isLocked;
                break;
            default: break;
        }
    }
}
