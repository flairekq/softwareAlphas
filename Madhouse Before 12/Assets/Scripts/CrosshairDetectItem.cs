using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairDetectItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private RaycastHit hit;
    [SerializeField] private LayerMask layer;
    private DisplayUI previousItem;
    private DisplayUI currentItem;

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            Ray ray = cam.ScreenPointToRay(this.transform.position);
            if (Physics.Raycast(ray, out hit, 2.5f, layer))
            {
                // Debug.Log("hit");
                DisplayUI display = hit.collider.GetComponent<DisplayUI>();
                if (display != null)
                {
                    display.SetPlayerPosition(cam.transform.parent.position);
                    previousItem = currentItem;
                    currentItem = display;
                    currentItem.SetDisplayInfo(true);
                    if (previousItem != null && currentItem != previousItem)
                    {
                        previousItem.SetDisplayInfo(false);
                    }
                }
            }
            else
            {
                previousItem = currentItem;
                if (previousItem != null)
                {
                    previousItem.SetDisplayInfo(false);
                }
            }
        }
    }
}
