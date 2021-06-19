using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGeneratorController : MonoBehaviour
{
    private bool isInUse = false;
    private DisplayUI displayUI;
    // Start is called before the first frame update
    void Start()
    {
        displayUI = GetComponent<DisplayUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleActivation(bool isActivate)
    {
        isInUse = isActivate;
        if (isInUse)
        {
            displayUI.SetDisplayInfo(false);
        }
        else
        {

        }
    }

    public bool IsInUse()
    {
        return isInUse;
    }
}
