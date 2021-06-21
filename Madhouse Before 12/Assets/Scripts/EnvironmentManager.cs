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

    public void TogglePower(bool val)
    {
        EnvironmentManager.instance.PV.RPC("RPC_HandlePowerOnOff", RpcTarget.All, val);
    }

    [PunRPC]
    private void RPC_HandlePowerOnOff(bool val)
    {
        EnvironmentManager.instance.isPowerOn = val;
    }
}
