using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 6;
    public GameObject inventoryPanel;
    public InventoryUI inventoryUI;
    private TogglePlayerCursor togglePlayerCursor;

    public List<GameObject> items = new List<GameObject>();
    public bool Add(GameObject item)
    {

        if (items.Count == space)
        {
            Debug.Log("Not enough room");
            return false;
        }
        items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(GameObject item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        togglePlayerCursor = GetComponent<TogglePlayerCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        if (inventoryPanel.activeSelf)
        {

            togglePlayerCursor.ChangeToCursor();
            inventoryUI.ReloadUI(gameObject);
        }
        else
        {
            togglePlayerCursor.ChangeToPlayer();
        }
    }
}
