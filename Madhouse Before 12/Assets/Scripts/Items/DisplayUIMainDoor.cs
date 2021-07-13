using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUIMainDoor : DisplayUI
{
    public override string OpenDoor(Vector3 pos, Inventory inventory)
    {
        if (!EnvironmentManager.instance.isMainDoorUnlocked)
        {
            return "You need to enter the correct code";
        }

        GameController.instance.KeypadGameOver();
        return "successful";
    }
}
