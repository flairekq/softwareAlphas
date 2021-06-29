using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairDetectItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private RaycastHit hit;
    [SerializeField] private LayerMask layer;
    public DisplayUI previousItem;
    public DisplayUI currentItem;
    private Ray ray;
    // in seconds
    private float interval = 0.4f; 
    private float timer = 1f;

    // Update is called once per frame
    void Update()
    {
        // don't allow an interval smaller than the frame
        float usedInterval = interval;
        if (Time.deltaTime > usedInterval) {
            usedInterval = Time.deltaTime;
        }
        
        if (timer >= usedInterval && cam != null)
        {
            timer = 0;
            ray = cam.ScreenPointToRay(this.transform.position);
            if (Physics.Raycast(ray, out hit, 2f, layer))
            {
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
        timer += Time.deltaTime;
    }
}
