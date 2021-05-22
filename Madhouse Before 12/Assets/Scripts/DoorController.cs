﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator doorAnim;
    public string roomName;
    public int roomLocation;
    public bool isClear = false;
    public GameObject inRoomTrigger;
    private InRoomTriggerController inRoomTriggerController;
    // public bool isInsideRoom = false;

    EnvironmentManager envManager;

    private void OnTriggerEnter(Collider other)
    {
        bool canOpenDoor = true;
        // if player has not entered room
        // check if player can enter this room
        if (!inRoomTriggerController.isInRoom)
        {
            if (roomLocation == 1 || roomLocation == 2)
            {
                if (!envManager.isClearBasement) {
                    canOpenDoor = false;
                }
            }
        }

        if (canOpenDoor)
        {
            doorAnim.SetBool("isOpening", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        doorAnim.SetBool("isOpening", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = this.transform.parent.GetComponent<Animator>();
        envManager = EnvironmentManager.instance;
        inRoomTriggerController = inRoomTrigger.GetComponent<InRoomTriggerController>();
    }
}
