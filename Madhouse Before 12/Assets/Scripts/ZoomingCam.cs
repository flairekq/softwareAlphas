using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomingCam : MonoBehaviour
{
    int zoom = 30;
    int normal = 60;
    float smooth = 5;
    private bool isZoomed = false;
    private Camera cam;
    private TogglePlayerCursor togglePlayerCursor;
    private bool hasChanged = true;

    void Start()
    {
        cam = GetComponent<Camera>();
        togglePlayerCursor = transform.parent.GetComponent<TogglePlayerCursor>();
    }

    void Update()
    {
        if (isZoomed)
        {
            transform.LookAt(EnvironmentManager.instance.powerGenerator);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * smooth);
            if (!hasChanged)
            {
                togglePlayerCursor.ChangeToCursorZoomIn();
                hasChanged = true;
            }
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normal, Time.deltaTime * smooth);
            if (!hasChanged)
            {
                togglePlayerCursor.ChangeToPlayerZoomOut();
                hasChanged = true;
            }
        }
    }

    public void ToggleZoom()
    {

        isZoomed = !isZoomed;
        hasChanged = false;
    }

    public bool IsZoomedIn() {
        return isZoomed;
    }
}
