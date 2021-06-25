using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private SwitchController[] switches;
    private bool isPartiallyOn = false;
    private float timeCounter = 5f;
    private List<SwitchController> switchesThatAreOn = new List<SwitchController>();

    // Update is called once per frame
    void Update()
    {
        int noOfSwitchOn = 0;
        switchesThatAreOn.Clear();
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].IsSwitchOn())
            {
                noOfSwitchOn++;
                switchesThatAreOn.Add(switches[i]);
            }
        }

        if (noOfSwitchOn == switches.Length)
        {
            isPartiallyOn = false;
            if (!EnvironmentManager.instance.isProjectorOn)
            {
                EnvironmentManager.instance.ToggleProjector(true);
            }
        }
        else if (noOfSwitchOn == 0)
        {
            isPartiallyOn = false;
            if (EnvironmentManager.instance.isProjectorOn)
            {
                EnvironmentManager.instance.ToggleProjector(false);
                Projector.instance.OffProjector();
            }
        }
        else
        {
            isPartiallyOn = true;
            if (EnvironmentManager.instance.isProjectorOn)
            {
                EnvironmentManager.instance.ToggleProjector(false);
                Projector.instance.OffProjector();
            }
        }

        if (isPartiallyOn && timeCounter <= 0)
        {
            Debug.Log("time is up");
            timeCounter = 5f;
            foreach (SwitchController sc in switchesThatAreOn)
            {
                sc.OffSwitch();
            }
        }
        else if (isPartiallyOn)
        {
            timeCounter -= Time.deltaTime;
        }
    }
}
