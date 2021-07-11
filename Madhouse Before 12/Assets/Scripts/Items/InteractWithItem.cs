using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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
    private DisplayInformation displayInformation;

    [SerializeField] private Image examineCanvasOldPaperImage;
    [SerializeField] private Text examineCanvasOldPaperText;
    [SerializeField] private Image examineCanvasItemImage;
    [SerializeField] private Image crossHairImage;
    [SerializeField] private CrosshairDetectItem crosshairDetectItem;
    [SerializeField] private Image examineCanvasDiaryClueImage;
    [SerializeField] private Text examineCanvasDiaryClueText;
    private ZoomingCam zoomingCam;
    private bool isTooCloseOrFar = false;
    private PhotonView PV;

    void Awake()
    {
        PV = gameObject.transform.parent.GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            mainCamera = GetComponent<Camera>();
            inventory = gameObject.transform.parent.GetComponent<Inventory>();
            togglePlayerCursor = gameObject.transform.parent.GetComponent<TogglePlayerCursor>();
            displayInformation = gameObject.transform.parent.GetComponentInChildren<DisplayInformation>();
            zoomingCam = GetComponent<ZoomingCam>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        SelectItemToInteractWithFromRay();

        // examine or use
        if (Input.GetKeyDown(KeyCode.E) && itemDisplayUI != null && (itemDisplayUI.IsMouseOvering() || (itemDisplayUI.type.Equals("PowerGenerator") && zoomingCam.IsZoomedIn())))
        {
            if (itemDisplayUI.type == "Note" || itemDisplayUI.type == "DiaryClue")
            {
                NoteAppear note = itemBeingPickedUp.GetComponent<NoteAppear>();
                if (note != null)
                {
                    if (itemDisplayUI.type == "Note")
                    {
                        note.RetrievePlayerCanvas(examineCanvasOldPaperImage, examineCanvasOldPaperText);
                    }
                    else
                    {
                        note.RetrievePlayerCanvas(examineCanvasDiaryClueImage, examineCanvasDiaryClueText);
                    }
                    note.ToggleNote();
                    isExaminingItem = note.isExaminingNote();
                    if (isExaminingItem)
                    {
                        togglePlayerCursor.ChangeToCursor();
                    }
                    else
                    {
                        togglePlayerCursor.ChangeToPlayer();
                    }
                }
            }
            else if (itemDisplayUI.type == "DrawerTop")
            {
                // Animator animator = itemDisplayUI.GetComponent<Animator>();
                // animator.SetBool("isTopOpen", !animator.GetBool("isTopOpen"));
                DisplayUIDrawer drawer = itemDisplayUI.GetComponent<DisplayUIDrawer>();
                // drawer.ToggleDrawer("isTopOpen");
                drawer.ToggleDrawer(drawer.isTopOpenId);
            }
            else if (itemDisplayUI.type == "DrawerBtm")
            {
                // Animator animator = itemDisplayUI.GetComponent<Animator>();
                // animator.SetBool("isBtmOpen", !animator.GetBool("isBtmOpen"));
                DisplayUIDrawer drawer = itemDisplayUI.GetComponent<DisplayUIDrawer>();
                // drawer.ToggleDrawer("isBtmOpen");
                drawer.ToggleDrawer(drawer.isBtmOpenId);
            }
            else if (itemDisplayUI.type == "Keypad")
            {
                if (EnvironmentManager.instance.isPowerOn)
                {
                    CanvasInteract keypadCanvas = itemDisplayUI.GetComponent<CanvasInteract>();
                    if (togglePlayerCursor.IsInCursorMode() && keypadCanvas.IsCanvasOn())
                    {
                        keypadCanvas.CanvasOff();
                        togglePlayerCursor.ChangeToPlayer();
                        return;
                    }

                    if (keypadCanvas.IsCanvasOn())
                    {
                        // Debug.Log("Other player is using");
                        displayInformation.DisplayText("Other player is using");
                    }
                    else
                    {
                        itemDisplayUI.transform.parent.GetComponent<KeyController>().ChangeActiveCharacter(gameObject.transform.parent.gameObject);
                        togglePlayerCursor.ChangeToCursor();
                        keypadCanvas.CanvasOn();
                    }
                }
                else
                {
                    displayInformation.DisplayText("Power is not on");
                }
            }
            else if (itemDisplayUI.type == "Door" || itemDisplayUI.type == "MainDoor")
            {
                string msg = itemDisplayUI.OpenDoor(this.transform.parent.position, inventory);
                if (!msg.Equals("successful"))
                {
                    displayInformation.DisplayText(msg);
                }
            }
            else if (itemDisplayUI.type == "Switch")
            {
                SwitchController sc = itemDisplayUI.GetComponent<SwitchController>();
                sc.ToggleSwitch();
            }
            else if (itemDisplayUI.type == "PowerGenerator")
            {
                PowerGeneratorController pgc = itemDisplayUI.GetComponent<PowerGeneratorController>();
                if (pgc.IsInUse() && !zoomingCam.IsZoomedIn())
                {
                    displayInformation.DisplayText("Other player is using");
                }
                else if (pgc.IsInUse() && zoomingCam.IsZoomedIn())
                {
                    zoomingCam.ToggleZoom();
                    pgc.ToggleActivation(false, null);
                }
                else
                {
                    zoomingCam.ToggleZoom();
                    pgc.ToggleActivation(true, mainCamera);
                }
            }
            else if (itemDisplayUI.type == "Projector")
            {
                if (EnvironmentManager.instance.isProjectorOn)
                {
                    // Projector projector = itemDisplayUI.GetComponent<Projector>();
                    // projector.ToggleProjectorDisplay();
                    Projector.instance.ToggleProjectorDisplay();
                }
                else
                {
                    displayInformation.DisplayText("Projector is not on");
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
                    if (isExaminingItem)
                    {
                        togglePlayerCursor.ChangeToCursor();
                    }
                    else
                    {
                        togglePlayerCursor.ChangeToPlayer();
                    }
                }
            }
        }

        // take
        if (Input.GetKeyDown(KeyCode.Q) && itemBeingPickedUp != null && !isExaminingItem && itemDisplayUI.IsMouseOvering())
        {
            if (itemBeingPickedUp.PickUp(inventory))
            {
                DrawerItem drawerItem = itemBeingPickedUp.GetComponent<DrawerItem>();
                if (drawerItem != null)
                {
                    drawerItem.isPickedUp = true;
                }

                DiaryClue diaryClue = itemBeingPickedUp.GetComponent<DiaryClue>();
                if (diaryClue != null && !diaryClue.isPickedBefore)
                {
                    diaryClue.Picked();
                    SecretDiary.instance.AddWord(diaryClue.word);
                    displayInformation.DisplayText("Strangely, words has started to appear on the scroll.");
                    displayInformation.DisplayText("I should try and find more.");
                }
            }
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) && isTooCloseOrFar)
        {
            displayInformation.DisplayText("Too far/close");
        }
    }

    void SelectItemToInteractWithFromRay()
    {
        // ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // ray = mainCamera.ScreenPointToRay(crossHairImage.transform.position);
        // if (Physics.Raycast(ray, out hit, 2f, layer))
        // {
        //     DisplayUI display = hit.collider.GetComponent<DisplayUI>();

        //     if (display != null)
        //     {
        //         // ignore vertical distance
        //         Vector3 temp = new Vector3(display.transform.position.x, this.transform.parent.position.y, display.transform.position.z);
        //         float distance = Vector3.Distance(temp, this.transform.parent.position);
        //         if ((distance <= 1.8f && !display.type.Equals("PowerGenerator")) || (distance >= 1.2f && distance <= 2f && display.type.Equals("PowerGenerator")))
        //         {
        //             isTooCloseOrFar = false;
        //             itemDisplayUI = display;

        //             ItemPickup item = hit.collider.GetComponent<ItemPickup>();
        //             if (item != null)
        //             {
        //                 itemBeingPickedUp = item;
        //             }
        //             return;
        //         }
        //         else
        //         {
        //             itemDisplayUI = null;
        //             itemBeingPickedUp = null;
        //             isTooCloseOrFar = true;
        //         }
        //     }
        // }

        if (crosshairDetectItem.currentItem != null)
        {
            DisplayUI display = crosshairDetectItem.currentItem;
            // ignore vertical distance
            Vector3 temp = new Vector3(display.transform.position.x, this.transform.parent.position.y, display.transform.position.z);
            float distance = Vector3.Distance(temp, this.transform.parent.position);
            if ((distance <= 1.8f && !display.type.Equals("PowerGenerator")) || (distance >= 1.2f && distance <= 2f && display.type.Equals("PowerGenerator")))
            {
                isTooCloseOrFar = false;
                itemDisplayUI = display;

                // ItemPickup item = hit.collider.GetComponent<ItemPickup>();
                ItemPickup item = display.GetComponent<ItemPickup>();
                if (item != null)
                {
                    itemBeingPickedUp = item;
                }
                return;
            }
            else
            {
                itemDisplayUI = null;
                itemBeingPickedUp = null;
                isTooCloseOrFar = true;
            }
        }

        // if (itemBeingPickedUp != null)
        // {
        //     if (itemDisplayUI.type == "Note" || itemDisplayUI.type == "DiaryClue")
        //     {
        //         NoteAppear note = itemBeingPickedUp.GetComponent<NoteAppear>();
        //         if (note != null)
        //         {
        //             if (itemDisplayUI.type == "Note")
        //             {
        //                 note.RetrievePlayerCanvas(examineCanvasOldPaperImage, examineCanvasOldPaperText);
        //             }
        //             else
        //             {
        //                 note.RetrievePlayerCanvas(examineCanvasDiaryClueImage, examineCanvasDiaryClueText);
        //             }
        //             note.CloseNote();
        //         }
        //     }
        //     // else if (itemDisplayUI.type == "Keypad")
        //     // {
        //     //     CanvasInteract keypadCanvas = itemDisplayUI.GetComponent<CanvasInteract>();
        //     //     keypadCanvas.CanvasOff();
        //     // }
        //     else if (itemDisplayUI.type != "DrawerTop" && itemDisplayUI.type != "DrawerBtm")
        //     {
        //         ItemAppear ia = itemBeingPickedUp.GetComponent<ItemAppear>();
        //         if (ia != null)
        //         {
        //             ia.RetrievePlayerCanvas(examineCanvasItemImage);
        //             ia.CloseItem();
        //         }
        //     }

        //     itemBeingPickedUp = null;
        //     itemDisplayUI = null;
        //     isExaminingItem = false;
        // }

    }
}

