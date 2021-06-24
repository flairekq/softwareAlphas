using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInformation : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text infoText;
    private float displayTime = 2.5f;
    private bool isDisplay = true;
    private List<string> msges = new List<string>();

    void Update()
    {
        if (isDisplay) {
            if (msges.Count > 0) {
                infoText.text = msges[0];
                panel.SetActive(true);
            }
            
            if (displayTime <= 0f) {
                if (msges.Count > 0) {
                    msges.RemoveAt(0);
                }
                
                if (msges.Count == 0) {
                    panel.SetActive(false);
                    isDisplay = false;
                }
                displayTime = 2.5f;
            } else {
                displayTime -= Time.deltaTime;
            }
        }
    }

    public void DisplayText(string text)
    {
        msges.Add(text);
        // infoText.text = text;
        // panel.SetActive(true);
        isDisplay = true;
    }
}
