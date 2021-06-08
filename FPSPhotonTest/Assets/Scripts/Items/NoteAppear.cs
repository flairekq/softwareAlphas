using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteAppear : MonoBehaviour
{
    private Image noteImage;
    private Text text;
    private string content;

    void Start()
    {
        content = GetComponent<Item>().content;
    }

    public void RetrievePlayerCanvas(Image image, Text txt)
    {
        noteImage = image;
        text = txt;
    }

    public void ToggleNote()
    {
        noteImage.enabled = !noteImage.enabled;
        text.enabled = !text.enabled;
        text.text = content;
    }

    public void CloseNote()
    {
        noteImage.enabled = false;
        text.enabled = false;
    }

    public bool isExaminingNote()
    {
        return noteImage.enabled;
    }

}
