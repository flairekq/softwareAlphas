using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PreventSeeingThrough : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    private PhotonView PV;
    // [SerializeField] LayerMask gunLayer;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            ChangeLayer();
        }
    }

    private void ChangeLayer()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].layer = LayerMask.NameToLayer("Gun");
        }
    }
}
