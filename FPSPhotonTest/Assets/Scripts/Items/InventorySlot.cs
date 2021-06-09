using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    GameObject item;
    Item details;
    private GameObject activeChar;

    [SerializeField] private Text noteText = null;
    [SerializeField] private Text displayNameText = null;
    [SerializeField] private Image focusPanelImage = null;

    public void AddItem(GameObject newItem, GameObject character)
    {
        activeChar = character;
        item = newItem;
        details = item.GetComponent<Item>();
        // Debug.Log("add item " + details);
        icon.sprite = details.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        activeChar = null;
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public bool IsEmpty()
    {
        return item == null;
    }

    public void OnRemoveButton()
    {
        item.transform.position = new Vector3(activeChar.transform.position.x, 0.5f, activeChar.transform.position.z);
        item.GetComponent<ItemPickup>().MakeVisible();

        activeChar.GetComponent<Inventory>().Remove(item);
        details.OffExamine();
    }

    public void UseItem()
    {
        // Debug.Log("using item " + details);
        if (item != null)
        {
            details.Examine(noteText, displayNameText, focusPanelImage);
        }
    }
}
