using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public CharacterManager CharacterManager;
    
    void Awake()
    {
        if (Instance) // checks if another room manager already exists
        {
            Destroy(RoomManager.Instance.gameObject); // there can only be one
            return;
        }
        DontDestroyOnLoad(gameObject); // The only one
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        // if (scene.buildIndex == 1) // in the game scene
        if (scene.buildIndex >= 1 && scene.buildIndex <= 3)
        {
            if(CharacterManager.chooseAvatar() == 1)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager1"), Vector3.zero, Quaternion.identity);
            }
            if(CharacterManager.chooseAvatar() == 0)
            {
                 PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            }
        
        }
    }
}
