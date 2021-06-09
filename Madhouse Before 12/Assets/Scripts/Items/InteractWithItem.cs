using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithItem : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Camera mainCamera;
    [SerializeField] private LayerMask layer;

    private ItemPickup itemBeingPickedUp;
    private DisplayUI itemDisplayUI;
    private Inventory inventory;
    private TogglePlayerCursor togglePlayerCursor;
    private bool isExaminingItem = false;

    [SerializeField] private Image examineCanvasOldPaperImage;
    [SerializeField] private Text examineCanvasOldPaperText;
    [SerializeField] private Image examineCanvasItemImage;
    [SerializeField] private Image crossHairImage;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
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
                    note.RetrievePlayerCanvas(examineCanvasOldPaperImage, examineCanvasOldPaperText);
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
                if (keypadCanvas.IsCanvasOn())
                {
                    // keypadCanvas.CanvasOff();
                    // togglePlayerCursor.ChangeToPlayer();
                    Debug.Log("Other player is using");
                }
                else
                {
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
                    ia.RetrievePlayerCanvas(examineCanvasItemImage);
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
        // ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        ray = mainCamera.ScreenPointToRay(crossHairImage.transform.position);
        if (Physics.Raycast(ray, out hit, 2.5f, layer))
        {
            DisplayUI display = hit.collider.GetComponent<DisplayUI>();

            if (display != null)
            {
                // ignore vertical distance
                Vector3 temp = new Vector3(display.transform.position.x, this.transform.parent.transform.position.y, display.transform.position.z);
                if (Vector3.Distance(temp, this.transform.parent.transform.position) <= 1.5f) {
                    itemDisplayUI = display;

                    ItemPickup item = hit.collider.GetComponent<ItemPickup>();
                    if (item != null)
                    {
                        itemBeingPickedUp = item;
                    }
                    return;
                }
                
            }
        }

        if (itemBeingPickedUp != null)
        {
            if (itemDisplayUI.type == "Note")
            {
                NoteAppear note = itemBeingPickedUp.GetComponent<NoteAppear>();
                if (note != null)
                {
                    note.RetrievePlayerCanvas(examineCanvasOldPaperImage, examineCanvasOldPaperText);
                    note.CloseNote();
                }
            }
            else if (itemDisplayUI.type != "DrawerTop" && itemDisplayUI.type != "DrawerBtm")
            {
                ItemAppear ia = itemBeingPickedUp.GetComponent<ItemAppear>();
                if (ia != null)
                {
                    ia.RetrievePlayerCanvas(examineCanvasItemImage);
                    ia.CloseItem();
                }
            }

            itemBeingPickedUp = null;
            itemDisplayUI = null;
            isExaminingItem = false;
        }

    }
}

