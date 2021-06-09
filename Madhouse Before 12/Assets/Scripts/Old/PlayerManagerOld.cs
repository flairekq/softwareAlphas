using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManagerOld : MonoBehaviour
{
    #region Singleton

    public static PlayerManagerOld instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public GameObject player;

    public void KillPlayer() {
        // get current scene to restart it  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}