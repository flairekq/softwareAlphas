using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithItem : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Camera mainCamera;
    [SerializeField] LayerMask layer;

    private ItemPickup itemBeingPickedUp;
    private Inventory inventory;
    private bool isExaminingItem = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        inventory = gameObject.transform.parent.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectItemBeingPickedUpFromRay();

        // examine
        if (Input.GetKeyDown(KeyCode.E) && itemBeingPickedUp != null)
        {
            // Debug.Log("examine");
            NoteAppear note = itemBeingPickedUp.GetComponent<NoteAppear>();
            if (note != null)
            {
                note.ToggleNote();
                isExaminingItem = note.isExaminingNote();
            }
        }

        // take
        if (Input.GetKeyDown(KeyCode.Q) && itemBeingPickedUp != null && !isExaminingItem)
        {
            itemBeingPickedUp.PickUp(inventory);
        }
    }

    void SelectItemBeingPickedUpFromRay()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1f, layer))
        {
            // Debug.Log("pick up item " + hit.collider.name);
            ItemPickup item = hit.collider.GetComponent<ItemPickup>();
            if (item != null)
            {
                itemBeingPickedUp = item;
                return;
            }
        }

        if (itemBeingPickedUp != null)
        {
            NoteAppear note = itemBeingPickedUp.GetComponent<NoteAppear>();
            if (note != null)
            {
                note.CloseNote();
            }
            itemBeingPickedUp = null;
            isExaminingItem = false;
        }

    }
}

