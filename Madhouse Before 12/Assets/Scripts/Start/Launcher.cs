using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    [SerializeField] InputField roomNameInputField;

    [SerializeField] InputField playerNameInputField;
    [SerializeField] Text errorText;
    [SerializeField] Text roomNameText;

    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;

    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;

    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject warningDialog;
    [SerializeField] Text warningDialogText;
    [SerializeField] int maxPlayerPerRoom;
    private PhotonView PV;

   

    void Awake()
    {
        Instance = this;
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Connecting to Master");
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        // Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("main");
        // Debug.Log("Joined Lobby");
        // PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }


    public void inputName()
    {
        if (string.IsNullOrEmpty(playerNameInputField.text))
        {
            warningDialogText.text = "Player name cannot be empty";
            ToggleConfirmDialog(true);
            return;
        }

        PhotonNetwork.NickName = playerNameInputField.text;
        MenuManager.Instance.OpenMenu("play");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            warningDialogText.text = "Room name cannot be empty";
            ToggleConfirmDialog(true);
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        if (players.Length == maxPlayerPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        // only able to see if its the host
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        warningDialogText.text = "Room Creation Failed: " + message;
        ToggleConfirmDialog(true);
        // MenuManager.Instance.OpenMenu("error");
    }

    public void ToggleConfirmDialog(bool isShow)
    {
        warningDialog.SetActive(isShow);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    // public override void OnLeftRoom()
    // {
    //     MenuManager.Instance.OpenMenu("loading");
    // }

    public void JoinRoom(RoomInfo info)
    {
        if (info.IsOpen)
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.Instance.OpenMenu("loading");
        }
        else
        {
            warningDialogText.text = "Room is full";
            ToggleConfirmDialog(true);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Debug.Log("updating");
        // clear the list
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                // Debug.Log(roomList[i].Name + " is available");
                Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            }
            // else {
            //     Debug.Log(roomList[i].Name + " removed from list");
            // }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        // as 1 is the build index of the game scene which we had set in the build settings
        // PhotonNetwork.LoadLevel(1);

        // shows loading screen to all players in the same room as host
        PhotonNetwork.CurrentRoom.RemovedFromList = true;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PV.RPC("ShowLoadingScreenToAll", RpcTarget.All);
        int level = Random.Range(1, 4);
        PhotonNetwork.LoadLevel(level);
    }

    [PunRPC]
    private void ShowLoadingScreenToAll()
    {
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (this.CanRecoverFromDisconnect(cause))
        {
            this.Recover();
        }
    }

    private bool CanRecoverFromDisconnect(DisconnectCause cause)
    {
        switch (cause)
        {
            // the list here may be non exhaustive and is subject to review
            case DisconnectCause.Exception:
            case DisconnectCause.ServerTimeout:
            case DisconnectCause.ClientTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByServerReasonUnknown:
                return true;
        }
        return false;
    }

    private void Recover()
    {
        if (!PhotonNetwork.ReconnectAndRejoin())
        {
            Debug.LogError("ReconnectAndRejoin failed, trying Reconnect");
            if (!PhotonNetwork.Reconnect())
            {
                Debug.LogError("Reconnect failed, trying ConnectUsingSettings");
                if (!PhotonNetwork.ConnectUsingSettings())
                {
                    Debug.LogError("ConnectUsingSettings failed");
                }
            }
        }
    }
}
