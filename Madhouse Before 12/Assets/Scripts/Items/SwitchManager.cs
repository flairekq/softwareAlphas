using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private SwitchController[] switches;
    private bool isPartiallyOn = false;
    private float timeCounter = 10f;
    private List<SwitchController> switchesThatAreOn = new List<SwitchController>();

    // // Start is called before the first frame update
    // void Start()
    // {

    // }

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

        if (noOfSwitchOn == switches.Length || noOfSwitchOn == 0)
        {
            isPartiallyOn = false;
        } else {
            isPartiallyOn = true;
        }

        if (isPartiallyOn && timeCounter <= 0)
        {
            Debug.Log("time is up");
            timeCounter = 10f;
            foreach (SwitchController sc in switchesThatAreOn) {
                sc.OffSwitch();
            }
        }
        else if (isPartiallyOn)
        {
            timeCounter -= Time.deltaTime;
        }
        else { }
    }
}
