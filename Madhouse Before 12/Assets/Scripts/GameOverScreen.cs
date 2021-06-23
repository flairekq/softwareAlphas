using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOverScreen : MonoBehaviourPunCallbacks
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        PhotonNetwork.LeaveRoom();
        // PhotonNetwork.LoadLevel(0);
        // PhotonNetwork.JoinLobby();
    }

    public override void OnLeftRoom()
    {
        // MenuManager.Instance.OpenMenu("title");
        if (RoomManager.Instance)
        {
            Destroy(RoomManager.Instance.gameObject); // there can only be one
        }
        PhotonNetwork.LoadLevel(0);
    }
}
