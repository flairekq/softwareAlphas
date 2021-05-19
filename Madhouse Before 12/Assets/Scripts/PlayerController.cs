using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    Camera cam;
    CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // if press left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // // Move our player to what we hit
                // Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                // Stop focusing any objects
                RemoveFocus();
            }
        }

        // if press right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check if we hit an interactable
                // If we did set it as our focus
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
        
                    if (interactable is Enemy) {
                        Enemy e = (Enemy)interactable;
                        e.Interact();
                    }
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    void RemoveFocus()
    {
        focus = null;
    }
}
