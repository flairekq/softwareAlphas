using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private List<int> players = new List<int>();
    public List<GameObject> gameObjectPlayers = new List<GameObject>();
    private PhotonView PV;
    public bool isGameOver = false;
    [SerializeField] GameOverScreen gameOverScreen;

    void Awake()
    {
        instance = this;
        instance.PV = GetComponent<PhotonView>();
    }

    public void AddPlayer(int playerID)
    {
        EnvironmentManager.instance.PV.RPC("RPC_HandlePlayer", RpcTarget.All, playerID);
    }

    [PunRPC]
    private void RPC_HandlePlayer(int playerID)
    {
        GameController.instance.players.Add(playerID);
    }

    public void GameOver(bool isTimeup)
    {
        foreach (int id in players)
        {
            PhotonView playerPV = PhotonView.Find(id);
            playerPV.GetComponent<TogglePlayerCursor>().GameOver();
        }
        gameOverScreen.Show(isTimeup);
    }

    public void GetAllPlayers()
    {
        foreach (int id in players)
        {
            PhotonView playerPV = PhotonView.Find(id);
            GameController.instance.gameObjectPlayers.Add(playerPV.gameObject);
        }
    }
}
