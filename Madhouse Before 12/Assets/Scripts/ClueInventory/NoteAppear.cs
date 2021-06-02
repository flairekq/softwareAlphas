using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteAppear : MonoBehaviour
{
    [SerializeField] private Image noteImage;
    [SerializeField] private Text text;
    [SerializeField] private string content;

    private void Start() {
        text.text = content;
    }

    public void ToggleNote() {
        noteImage.enabled = !noteImage.enabled;
        text.enabled = !text.enabled;
    }

    public void CloseNote() {
        noteImage.enabled = false;
        text.enabled = false;
    }
}
