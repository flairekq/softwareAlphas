using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInformation : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text infoText;
    private float displayTime = 3f;
    private bool isDisplay = true;

    void Update()
    {
        if (isDisplay) {
            if (displayTime <= 0f) {
                panel.SetActive(false);
                isDisplay = false;
                displayTime = 3f;
            } else {
                displayTime -= Time.deltaTime;
            }
        }
    }

    public void DisplayText(string text)
    {
        infoText.text = text;
        panel.SetActive(true);
        isDisplay = true;
    }
}
