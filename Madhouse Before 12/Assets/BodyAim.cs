using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Photon.Pun;

public class BodyAim : MonoBehaviour
{
    public Transform AimLookAt;
    private PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            foreach (MultiAimConstraint component in GetComponentsInChildren<MultiAimConstraint>())
            {
                var data = component.data.sourceObjects;
                data.SetTransform(0, AimLookAt.transform);
                component.data.sourceObjects = data;
            }
            RigBuilder rigs = GetComponent<RigBuilder>();
            rigs.Build();
        }
    }
}

