using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAppear : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    private Sprite icon;

    void Start()
    {
        icon = GetComponent<Item>().GetExpandIcon();
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
