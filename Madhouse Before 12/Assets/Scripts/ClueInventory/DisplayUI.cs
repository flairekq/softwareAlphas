using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    public GameObject display;
    public float fadeTime;
    public bool displayInfo;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FadeCanvas();
    }

    void OnMouseOver() {
        displayInfo = true;
    }

    void OnMouseExit() {
        displayInfo = false;
    }

    void FadeCanvas() {
        // if (displayInfo) {
        //     canvas.alpha = 1f;
        //     canvas.interactable = true;
        //     canvas.blocksRaycasts = true;
        // } else {
        //     canvas.alpha = 0f;
        //     canvas.interactable = false;
        //     canvas.blocksRaycasts = false;
        // }
        if (displayInfo) {
            display.SetActive(true);
        } else {
            display.SetActive(false);
        }
    }

    // Ray ray;
    // RaycastHit hit;

    // [SerializeField] LayerMask layer;

    // void Update() {
    //     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (Physics.Raycast(ray, out hit, 200, layer)) {
    //         Debug.Log("hit");
    //         GameObject display = hit.collider.transform.parent.GetComponentInChildren<Interactable>().gameObject;
    //         display.SetActive(!display.activeSelf);
    //     }
    // }
}
