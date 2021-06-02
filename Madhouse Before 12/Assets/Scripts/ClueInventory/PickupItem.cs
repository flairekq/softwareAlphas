using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Camera mainCamera;
    [SerializeField] LayerMask layer;

    private Item itemBeingPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
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
            }
        }

        // take
        if (Input.GetKeyDown(KeyCode.Q) && itemBeingPickedUp != null)
        {
            Debug.Log("take");
        }
    }

    void SelectItemBeingPickedUpFromRay()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1f, layer))
        {
            // Debug.Log("pick up item " + hit.collider.name);
            Item item = hit.collider.GetComponent<Item>();
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
        }

    }
}

