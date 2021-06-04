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
    public GameObject crossHair;
    private PlayerMovement movement;
    private MouseLook mouseLook;
    private PlayerController playerController;

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
        movement = GetComponent<PlayerMovement>();
        mouseLook = GetComponentInChildren<MouseLook>();
        playerController = GetComponent<PlayerController>();
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
        crossHair.SetActive(!crossHair.activeSelf);

        if (inventoryPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            movement.enabled = false;
            mouseLook.enabled = false;
            playerController.enabled = false;
            inventoryUI.ReloadUI(gameObject);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            movement.enabled = true;
            mouseLook.enabled = true;
            playerController.enabled = true;
        }
    }
}
