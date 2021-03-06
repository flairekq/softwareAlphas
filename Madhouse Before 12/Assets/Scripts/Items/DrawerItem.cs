using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerItem : MonoBehaviour
{
    // [SerializeField] public GameObject item;
    public GameObject item;
    // public Transform item;
    [SerializeField] float zEndPoint;
    // [SerializeField] int frames;
    private float frames;
    public bool isPickedUp = false;
    public float increment = 0f;

    // Start is called before the first frame update
    void Start()
    {
        increment = zEndPoint / frames;
    }

    public void UpdateFrames()
    {
        frames = 1.0f / Time.smoothDeltaTime;
        increment = zEndPoint / frames;
    }
}
