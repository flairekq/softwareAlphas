using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerGeneratorController : MonoBehaviour
{
    private bool isInUse = false;
    private DisplayUI displayUI;
    private Animator animator;
    [SerializeField] Canvas examineUI;
    [SerializeField] private string correctCombi;
    private string combi = "";
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        displayUI = GetComponent<DisplayUI>();
        examineUI.enabled = false;
        animator = GetComponent<Animator>();
    }

    public void ToggleActivation(bool isActivate, Camera cam)
    {
        isInUse = isActivate;
        if (isInUse)
        {
            displayUI.SetDisplayInfo(false);
            examineUI.enabled = true;
            examineUI.worldCamera = cam;
        }
        else
        {
            examineUI.enabled = false;
            // examineUI.worldCamera = null;
            combi = "";
        }
    }

    public bool IsInUse()
    {
        return isInUse;
    }

    public void YellowButtonPressed()
    {
        Debug.Log("yellow pressed");
        combi += "Y";
    }

    public void BlueButtonPressed()
    {
        Debug.Log("blue pressed");
        combi += "B";
    }

    public void LeverButtonPressed()
    {
        if (animator.GetBool("isOn"))
        {
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isOn", false);
            EnvironmentManager.instance.TogglePower(false);
            // EnvironmentManager.instance.isPowerOn = false;
            // animator.SetBool("isOn", false);
            return;
        }

        if (combi.Equals(correctCombi) && !animator.GetBool("isSuccess"))
        {
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isSuccess", true);
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isOn", true);
            EnvironmentManager.instance.TogglePower(true);
            // EnvironmentManager.instance.isPowerOn = true;
            // animator.SetBool("isSuccess", true);
            // animator.SetBool("isOn", true);
        }
        else if (!combi.Equals(correctCombi))
        {
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isSuccess", false);
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isOn", true);
            EnvironmentManager.instance.TogglePower(false);
            // EnvironmentManager.instance.isPowerOn = false;
            // animator.SetBool("isSuccess", false);
            // animator.SetBool("isOn", true);
            combi = "";
        }
    }

    [PunRPC]
    private void RPC_HandlePowerGenerator(string animationName, bool val)
    {
        animator.SetBool(animationName, val);
    }
}
