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
    public PositionRange[] basementPositionRange;
    public GameObject[] firstFloorRooms;
    public PositionRange[] firstFloorPositionRange;
    public GameObject[] secondFloorRooms;
    public PositionRange[] secondFloorPositionRange;
    public bool isClearBasement = false;
    public Transform powerGenerator;
    
}
