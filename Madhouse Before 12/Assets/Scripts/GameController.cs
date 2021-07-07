using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
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
            }
        }
    }

    [PunRPC]
    private void RPC_HandlePlayer(int playerID)
    {
        GameController.instance.players.Add(playerID);
        Player[] players = PhotonNetwork.PlayerList;
        if (players.Length == GameController.instance.players.Count)
        {
            GameController.instance.isAllPlayersIn = true;
            // GenerateEnemies.instance.InitialSpawnEnemies();
        }
    }

    [PunRPC]
    private void RPC_CallInitialSpawn()
    {
        GameController.instance.hasInitialSpawn = true;
        GenerateEnemies.instance.InitialSpawnEnemies();
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
        gameOverScreen.Show(isTimeup);
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

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (this.CanRecoverFromDisconnect(cause))
        {
            this.Recover();
        }
        else
        {
            GameController.instance.GetAllPlayers();
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
            // Debug.LogError("ReconnectAndRejoin failed, trying Reconnect");
            if (!PhotonNetwork.Reconnect())
            {
                // Debug.LogError("Reconnect failed, trying ConnectUsingSettings");
                if (!PhotonNetwork.ConnectUsingSettings())
                {
                    // Debug.LogError("ConnectUsingSettings failed");
                    GameController.instance.GetAllPlayers();
                }
            }
        }
    }
}
