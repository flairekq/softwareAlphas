using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    public GameObject display;
    public bool displayInfo;
    public string type;
    public Text drawerUIText;
    protected Animator animator;

    protected Vector3 playerPos = Vector3.zero;
    public int isTopOpenId = 0;
    public int isBtmOpenId = 0;

    void Awake()
    {
        if (!type.Equals("MainDoor"))
        {
            animator = GetComponent<Animator>();
            // if (type.Equals("DrawerTop") || type.Equals("DrawerBtm"))
            // {
            //     isTopOpenId = Animator.StringToHash("isTopOpen");
            //     isBtmOpenId = Animator.StringToHash("isBtmOpen");
            // }
            // else if (type.Equals("Door"))
            // {
            //     isOpeningFrontId = Animator.StringToHash("isOpeningFront");
            //     isOpeningBackId = Animator.StringToHash("isOpeningBack");
            //     Debug.Log(isOpeningFrontId);
            // }
        }
    }

    // void Start()
    // {

    // }

    // Update is called once per frame
    void Update()
    {
        FadeCanvas();
    }

    // void OnMouseOver()
    // {
    //     displayInfo = true;
    // }

    // void OnMouseExit()
    // {
    //     displayInfo = false;
    // }

    public virtual void FadeCanvas()
    {
        if (displayInfo && !display.activeSelf)
        {
            display.SetActive(true);
            ToggleDrawerText();
        }
        else if (!displayInfo && display.activeSelf)
        {
            display.SetActive(false);
        }
    }

    void ToggleDrawerText()
    {
        if (type.Equals("DrawerTop"))
        {
            // if (animator.GetBool("isTopOpen"))
            if (animator.GetBool(isTopOpenId))
            {
                drawerUIText.text = "Close";
            }
            else
            {
                drawerUIText.text = "Open";
            }
        }
        else if (type.Equals("DrawerBtm"))
        {
            // if (animator.GetBool("isBtmOpen"))
            if (animator.GetBool(isBtmOpenId))
            {
                drawerUIText.text = "Close";
            }
            else
            {
                drawerUIText.text = "Open";
            }
        }
    }

    public void ShiftDisplayUI()
    {
        if (type.Equals("DrawerTop") || type.Equals("DrawerBtm"))
        {
            // if (animator.GetBool("isTopOpen") || animator.GetBool("isBtmOpen"))
            if (animator.GetBool(isTopOpenId) || animator.GetBool(isBtmOpenId))
            {
                display.transform.localPosition = new Vector3(display.transform.localPosition.x, display.transform.localPosition.y, display.transform.localPosition.z + 0.344f);
            }
            else
            {
                display.transform.localPosition = new Vector3(display.transform.localPosition.x, display.transform.localPosition.y, display.transform.localPosition.z - 0.344f);
            }
            displayInfo = true;
        }
    }

    public void HideUI()
    {
        displayInfo = false;
        display.SetActive(false);
    }

    public virtual bool IsMouseOvering()
    {
        return displayInfo;
    }

    public virtual void SetDisplayInfo(bool val)
    {
        displayInfo = val;
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        playerPos = pos;
    }

    public virtual string OpenDoor(Vector3 pos, Inventory inventory)
    {
        playerPos = pos;
        return "successful";
    }

}
