using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Item item;

    void Start()
    {
        // NoteAppear note = gameObject.GetComponent<NoteAppear>();
        // if (note != null) {
        //     item.content = note.GetContent();
        // }
        item = gameObject.GetComponent<Item>();
    }

    public void PickUp(Inventory inventory)
    {
        // Debug.Log("picking up " + item.name);
        bool wasPickedUp = inventory.Add(gameObject);
        if (wasPickedUp)
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }
}
