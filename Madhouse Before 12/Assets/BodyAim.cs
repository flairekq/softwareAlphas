using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BodyAim : MonoBehaviour
{
  public Transform AimLookAt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

