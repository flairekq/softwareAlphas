using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveGame : MonoBehaviourPunCallbacks
{
    public void GoToMainMenu()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        if (RoomManager.Instance)
        {
            Destroy(RoomManager.Instance.gameObject); // there can only be one
        }
        PhotonNetwork.LoadLevel(0);
    }
}
