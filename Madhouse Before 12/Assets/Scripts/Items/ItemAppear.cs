using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAppear : MonoBehaviour
{
    private Image itemImage;
    private Sprite icon;

    void Start()
    {
        icon = GetComponent<Item>().GetExpandIcon();
    }

    public void RetrievePlayerCanvas(Image img)
    {
        itemImage = img;
    }

    public void ToggleItem()
    {
        itemImage.enabled = !itemImage.enabled;
        itemImage.sprite = icon;
    }

    public void CloseItem()
    {
        itemImage.enabled = false;
    }

    public bool isExaminingItem()
    {
        return itemImage.enabled;
    }
}
