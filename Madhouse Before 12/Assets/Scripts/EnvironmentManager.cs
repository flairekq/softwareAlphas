using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    #region Singleton
    public static EnvironmentManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public GameObject[] basementRooms;
    public GameObject[] firstFloorRooms;
    public GameObject[] secondFloorRooms;
    public bool isClearBasement = false;
    
}
