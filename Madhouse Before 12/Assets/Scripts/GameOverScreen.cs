using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviourPunCallbacks
{
    [SerializeField] Text displayText;
    public void Show(bool isTimeup)
    {
        if (!isTimeup) {
            displayText.text = "Congratulations you managed to escape in time!";
        }
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
