using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    public static bool isOpen = false;
    public static bool isPlayerExiting = false;

    [SerializeField] private string doorOpen = "DoorOpen";
    [SerializeField] private string doorClose = "DoorClose";

    private void OnTriggerEnter(Collider other) {
        Debug.Log("closeTrigger: " + closeTrigger);
        if (other.CompareTag("Player")) {
            if (openTrigger && !isPlayerExiting) {
                myDoor.Play(doorOpen, 0, 0.0f);
                isOpen = true;
                //numOpening++;
                // openTrigger = false;
                // closeTrigger = true;
            } 
            // else if (closeTrigger && isOpen) {
            //     myDoor.Play(doorClose, 0, 0.0f);
            //     isOpen = false;
            // }
            else if (closeTrigger && !isOpen) {
                myDoor.Play(doorOpen, 0, 0.0f);
                isOpen = true;
                isPlayerExiting = true;
            }
                //gameObject.SetActive(false);
            // } else if (closeTrigger) {
            //     myDoor.Play(doorClose, 0, 0.0f);
            //     //gameObject.SetActive(false);
            // }
        } 
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            if (closeTrigger && isPlayerExiting) {
                myDoor.Play(doorClose, 0, 0.0f);
                isOpen = false;
                isPlayerExiting = false;
            } else if (closeTrigger && isOpen) {
                myDoor.Play(doorClose, 0, 0.0f);
                isOpen = false;
            }
        }
    }
}
