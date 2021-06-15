using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
    private bool isOn = true;

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F)) {
        //     isOn = !isOn;
        // }

        // lightObj.SetActive(isOn);
        if (Input.GetKeyDown(KeyCode.F) && !isOn) {
            lightObj.SetActive(true);
            isOn = true;
        } else if (Input.GetKeyDown(KeyCode.F) && isOn) {
            lightObj.SetActive(false);
            isOn = false;
        } else {}
    }
}
