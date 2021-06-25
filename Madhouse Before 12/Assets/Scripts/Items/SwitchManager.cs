using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    // public static SwitchManager instance;
    // [SerializeField] private int noOfSwitchesToBeOn = 0;

    public static SwitchManager instance;
    [SerializeField] private SwitchController[] switches;
    private bool isPartiallyOn = false;
    private float timeCounter = 10f;
    private List<SwitchController> switchesThatAreOn = new List<SwitchController>();

    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        int noOfSwitchOn = 0;
        instance.switchesThatAreOn.Clear();
        for (int i = 0; i < instance.switches.Length; i++)
        {
            if (instance.switches[i].IsSwitchOn())
            {
                noOfSwitchOn++;
                instance.switchesThatAreOn.Add(instance.switches[i]);
            }
        }

        if (noOfSwitchOn == instance.switches.Length)
        {
            instance.isPartiallyOn = false;
            if (!EnvironmentManager.instance.isProjectorOn)
            {
                EnvironmentManager.instance.ToggleProjector(true);
            }
        }
        else if (noOfSwitchOn == 0)
        {
            instance.isPartiallyOn = false;
            if (EnvironmentManager.instance.isProjectorOn)
            {
                EnvironmentManager.instance.ToggleProjector(false);
                Projector.instance.OffProjector();
            }
        }
        else
        {
            instance.isPartiallyOn = true;
            if (EnvironmentManager.instance.isProjectorOn)
            {
                EnvironmentManager.instance.ToggleProjector(false);
                Projector.instance.OffProjector();
            }
        }

        if (instance.isPartiallyOn && instance.timeCounter <= 0)
        {
            Debug.Log("time is up");
            instance.timeCounter = 10f;
            foreach (SwitchController sc in instance.switchesThatAreOn)
            {
                sc.OffSwitch();
            }
        }
        else if (instance.isPartiallyOn)
        {
            instance.timeCounter -= Time.deltaTime;
        }
    }
}
