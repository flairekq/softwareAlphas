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
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeCanvas();
    }

    void OnMouseOver()
    {
        displayInfo = true;
    }

    void OnMouseExit()
    {
        displayInfo = false;
    }

    void FadeCanvas()
    {
        if (displayInfo)
        {
            display.SetActive(true);
            ToggleDrawerText();
        }
        else
        {
            display.SetActive(false);
        }
    }

    void ToggleDrawerText()
    {
        if (type.Equals("DrawerTop"))
        {
            if (animator.GetBool("isTopOpen"))
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
            if (animator.GetBool("isBtmOpen"))
            {
                drawerUIText.text = "Close";
            }
            else
            {
                drawerUIText.text = "Open";
            }
        }
        else
        {

        }
    }

    public void ShiftDisplayUI()
    {
        if (type.Equals("DrawerTop") || type.Equals("DrawerBtm"))
        {
            if (animator.GetBool("isTopOpen") || animator.GetBool("isBtmOpen"))
            {
                display.transform.position = new Vector3(display.transform.position.x + 0.344f, display.transform.position.y, display.transform.position.z);
            }
            else
            {
                display.transform.position = new Vector3(display.transform.position.x - 0.344f, display.transform.position.y, display.transform.position.z);
            }
        }
        // else if (type.Equals("DrawerBtm"))
        // {
        //     if (animator.GetBool("isBtmOpen"))
        //     {
        //         display.transform.position = new Vector3(display.transform.position.x + 0.344f, display.transform.position.y, display.transform.position.z);
        //     }
        //     else
        //     {
        //         drawerUIText.text = "Open";
        //     }
        // }
        else
        {

        }
    }
}
