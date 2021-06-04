using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    public GameObject display;
    public bool displayInfo;

    // Update is called once per frame
    void Update()
    {
        FadeCanvas();
    }

    void OnMouseOver() {
        displayInfo = true;
    }

    void OnMouseExit() {
        displayInfo = false;
    }

    void FadeCanvas() {
        if (displayInfo) {
            display.SetActive(true);
        } else {
            display.SetActive(false);
        }
    }
}
