using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    InventorySlot[] slots;
    // public GameObject inventoryUI;
    private GameObject character;
    private Inventory inventory;
    [SerializeField] private Text noteText = null;
    [SerializeField] private Text displayNameText = null;
    [SerializeField] private Image focusPanelImage = null;

    void Start()
    {
        if (character != null)
        {
            ReloadUI(character);
        }
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUI()
    {
        // Debug.Log("UPDATING UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                // Debug.Log("updating ui for: " + character.name + " with item: " + inventory.items[i]);
                slots[i].AddItem(inventory.items[i], character);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        displayNameText.enabled = false;
        noteText.enabled = false;
        focusPanelImage.enabled = false;
    }

    public void ReloadUI(GameObject activeChar)
    {
        character = activeChar;
        // inventoryUI.SetActive(true);
        inventory = character.GetComponent<Inventory>();
        if (inventory.onItemChangedCallback != null)
        {
            inventory.onItemChangedCallback -= UpdateUI;
        }
        inventory.onItemChangedCallback += UpdateUI;

        UpdateUI();
    }
}
