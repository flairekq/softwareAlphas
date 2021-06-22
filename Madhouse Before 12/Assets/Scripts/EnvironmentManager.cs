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

    public GameObject[] basementRooms;
    public PositionRange[] basementPositionRange;
    public GameObject[] firstFloorRooms;
    public PositionRange[] firstFloorPositionRange;
    public GameObject[] secondFloorRooms;
    public PositionRange[] secondFloorPositionRange;
    public bool isClearBasement = false;
    public Transform powerGenerator;
    public bool isPowerOn = false;
    public bool isDayroomUnlocked = false;
    public bool isClassroomUnlocked = false;
    public bool isBasementMainUnlocked = false;

    public void TogglePower(bool val)
    {
        EnvironmentManager.instance.PV.RPC("RPC_HandlePowerOnOff", RpcTarget.All, val);
    }

    [PunRPC]
    private void RPC_HandlePowerOnOff(bool val)
    {
        EnvironmentManager.instance.isPowerOn = val;
    }

    public void UnlockDoor(string name) {
        EnvironmentManager.instance.PV.RPC("RPC_UnlockDoor", RpcTarget.All, name);
    }

    [PunRPC]
    private void RPC_UnlockDoor(string name)
    {
        switch (name)
        {
            case "dayroom":
                EnvironmentManager.instance.isDayroomUnlocked = true;
                break;
            case "classroom":
                EnvironmentManager.instance.isClassroomUnlocked = true;
                break;
            case "basement":
                EnvironmentManager.instance.isBasementMainUnlocked = true;
                break;
            default: break;
        }
    }
}
