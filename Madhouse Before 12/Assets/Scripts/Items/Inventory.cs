using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 6;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventoryUI inventoryUI;
    private TogglePlayerCursor togglePlayerCursor;
    private DisplayInformation displayInformation;

    public List<GameObject> items = new List<GameObject>();
    private PhotonView PV;

    public bool Add(GameObject item)
    {
        if (items.Count == space)
        {
            // Debug.Log("Not enough room");
            displayInformation.DisplayText("Your inventory is full");
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

    public bool isItemPicked(string name) {
        for (int i = 0; i < items.Count; i++) {
            Item item = items[i].GetComponent<Item>();
            if (item.name.Equals(name)) {
                return true;
            }
        }
        return false;
    }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // GameObject inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        // inventoryPanel = inventoryCanvas.transform.GetChild(0).gameObject;
        // inventoryUI = inventoryCanvas.GetComponent<InventoryUI>();
        togglePlayerCursor = GetComponent<TogglePlayerCursor>();
        displayInformation = GetComponentInChildren<DisplayInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
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
