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
    private DisplayUI itemDisplayUI;
    private Inventory inventory;
    private TogglePlayerCursor togglePlayerCursor;
    private bool isExaminingItem = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        inventory = gameObject.transform.parent.GetComponent<Inventory>();
        togglePlayerCursor = gameObject.transform.parent.GetComponent<TogglePlayerCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectItemToInteractWithFromRay();

        // examine
        if (Input.GetKeyDown(KeyCode.E) && itemDisplayUI != null && itemDisplayUI.IsMouseOvering())
        {
            if (itemDisplayUI.type == "Note")
            {
                NoteAppear note = itemBeingPickedUp.GetComponent<NoteAppear>();
                if (note != null)
                {
                    note.ToggleNote();
                    isExaminingItem = note.isExaminingNote();
                }
            }
            else if (itemDisplayUI.type == "DrawerTop")
            {
                Animator animator = itemDisplayUI.GetComponent<Animator>();
                animator.SetBool("isTopOpen", !animator.GetBool("isTopOpen"));
            }
            else if (itemDisplayUI.type == "DrawerBtm")
            {
                Animator animator = itemDisplayUI.GetComponent<Animator>();
                animator.SetBool("isBtmOpen", !animator.GetBool("isBtmOpen"));
            }
            else if (itemDisplayUI.type == "Keypad")
            {
                CanvasInteract keypadCanvas = itemDisplayUI.GetComponent<CanvasInteract>();
                if (keypadCanvas.IsCanvasOn()) {
                    // keypadCanvas.CanvasOff();
                    // togglePlayerCursor.ChangeToPlayer();
                    Debug.Log("Other player is using");
                } else {
                    itemDisplayUI.transform.parent.GetComponent<KeyController>().ChangeActiveCharacter(gameObject.transform.parent.gameObject);
                    keypadCanvas.CanvasOn();
                    togglePlayerCursor.ChangeToCursor();
                }                
            }
            else
            {
                ItemAppear ia = itemDisplayUI.GetComponent<ItemAppear>();
                if (ia != null)
                {
                    ia.ToggleItem();
                    isExaminingItem = ia.isExaminingItem();
                }
            }

        }

        // take
        if (Input.GetKeyDown(KeyCode.Q) && itemBeingPickedUp != null && !isExaminingItem && itemDisplayUI.IsMouseOvering())
        {
            itemBeingPickedUp.PickUp(inventory);
        }
    }

    void SelectItemToInteractWithFromRay()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1.2f, layer))
        {
            DisplayUI display = hit.collider.GetComponent<DisplayUI>();
            if (display != null)
            {
                itemDisplayUI = display;

                ItemPickup item = hit.collider.GetComponent<ItemPickup>();
                if (item != null)
                {
                    itemBeingPickedUp = item;
                }
                return;
            }
        }

        if (itemBeingPickedUp != null)
        {
            if (itemDisplayUI.type == "Note")
            {
                NoteAppear note = itemBeingPickedUp.GetComponent<NoteAppear>();
                if (note != null)
                {
                    note.CloseNote();
                }
            }
            else if (itemDisplayUI.type != "DrawerTop" && itemDisplayUI.type != "DrawerBtm")
            {
                ItemAppear ia = itemBeingPickedUp.GetComponent<ItemAppear>();
                if (ia != null)
                {
                    ia.CloseItem();
                }
            }

            itemBeingPickedUp = null;
            itemDisplayUI = null;
            isExaminingItem = false;
        }

    }
}

