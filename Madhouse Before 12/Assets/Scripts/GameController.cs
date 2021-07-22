using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private List<int> players = new List<int>();
    public List<GameObject> gameObjectPlayers = new List<GameObject>();
    private PhotonView PV;
    public bool isGameOver = false;
    [SerializeField] GameOverScreen gameOverScreen;
    private bool isAllPlayersIn = false;
    private bool hasInitialSpawn = false;

    void Awake()
    {
        instance = this;
        instance.PV = GetComponent<PhotonView>();
    }

    public void AddPlayer(int playerID)
    {
        EnvironmentManager.instance.PV.RPC("RPC_HandlePlayer", RpcTarget.All, playerID);
    }

    void Update()
    {
        if (!GameController.instance.hasInitialSpawn)
        {
            if (GameController.instance.isAllPlayersIn && PhotonNetwork.IsMasterClient)
            {
                GameController.instance.PV.RPC("RPC_CallInitialSpawn", RpcTarget.All);
                GenerateEnemies.instance.InitialSpawnEnemies();

            }
        }
    }

    [PunRPC]
    private void RPC_HandlePlayer(int playerID)
    {
        GameController.instance.players.Add(playerID);
        Player[] players = PhotonNetwork.PlayerList;
        // Debug.Log(players.Length);
        if (players.Length == GameController.instance.players.Count)
        {
            // Debug.Log("called");
            GameController.instance.isAllPlayersIn = true;
            // GenerateEnemies.instance.InitialSpawnEnemies();
        }
    }

    [PunRPC]
    private void RPC_CallInitialSpawn()
    {
        GameController.instance.hasInitialSpawn = true;
        // GenerateEnemies.instance.InitialSpawnEnemies();
    }

    public void GameOver(bool isTimeup)
    {
        foreach (int id in players)
        {
            PhotonView playerPV = PhotonView.Find(id);
            if (playerPV != null)
            {
                playerPV.GetComponent<TogglePlayerCursor>().GameOver();
            }
        }
        GameController.instance.isGameOver = true;
        gameOverScreen.Show(isTimeup);
    }

    public void KeypadGameOver()
    {
        GameController.instance.PV.RPC("RPC_HandleGameOver", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_HandleGameOver()
    {
        foreach (int id in players)
        {
            PhotonView playerPV = PhotonView.Find(id);
            if (playerPV != null)
            {
                playerPV.GetComponent<TogglePlayerCursor>().GameOver();
            }
        }
        gameOverScreen.Show(false);
    }

    public void GetAllPlayers()
    {
        GameController.instance.gameObjectPlayers.Clear();
        foreach (int id in players)
        {
            PhotonView playerPV = PhotonView.Find(id);
            if (playerPV != null)
            {
                GameController.instance.gameObjectPlayers.Add(playerPV.gameObject);
            }
        }
    }

    // public override void OnDisconnected(DisconnectCause cause)
    // {
    //     Debug.Log("disconnected");
    //     if (this.CanRecoverFromDisconnect(cause))
    //     {
    //         Debug.Log("recover");
    //         this.Recover();
    //     }
    //     else
    //     {
    //         Debug.Log("re-get");
    //         GameController.instance.GetAllPlayers();
    //     }
    // }

    // private bool CanRecoverFromDisconnect(DisconnectCause cause)
    // {
    //     switch (cause)
    //     {
    //         // the list here may be non exhaustive and is subject to review
    //         case DisconnectCause.Exception:
    //         case DisconnectCause.ServerTimeout:
    //         case DisconnectCause.ClientTimeout:
    //         case DisconnectCause.DisconnectByServerLogic:
    //         case DisconnectCause.DisconnectByServerReasonUnknown:
    //             return true;
    //     }
    //     return false;
    // }

    // private void Recover()
    // {
    //     if (!PhotonNetwork.ReconnectAndRejoin())
    //     {
    //         // Debug.LogError("ReconnectAndRejoin failed, trying Reconnect");
    //         if (!PhotonNetwork.Reconnect())
    //         {
    //             // Debug.LogError("Reconnect failed, trying ConnectUsingSettings");
    //             if (!PhotonNetwork.ConnectUsingSettings())
    //             {
    //                 // Debug.LogError("ConnectUsingSettings failed");
    //                 GameController.instance.GetAllPlayers();
    //             }
    //         }
    //     }
    // }
}
