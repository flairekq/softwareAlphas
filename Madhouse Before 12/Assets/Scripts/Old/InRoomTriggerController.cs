using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRoomTriggerController : MonoBehaviour
{
    public bool isInRoom = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRoom = false;
        }
    }
}
