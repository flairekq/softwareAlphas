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

    public int isOnId = 0;
    public int isSuccessId = 0;

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
        isOnId = Animator.StringToHash("isOn");
        isSuccessId = Animator.StringToHash("isSuccess");
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
        // Debug.Log("yellow pressed");
        combi += "Y";
    }

    public void BlueButtonPressed()
    {
        // Debug.Log("blue pressed");
        combi += "B";
    }

    public void LeverButtonPressed()
    {
        if (animator.GetBool("isOn"))
        {
            // PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isOn", false);
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, isOnId, false);
            EnvironmentManager.instance.TogglePower(false);
            // EnvironmentManager.instance.isPowerOn = false;
            // animator.SetBool("isOn", false);
            return;
        }

        // if (combi.Equals(correctCombi) && !animator.GetBool("isSuccess"))
        if (combi.Equals(correctCombi) && !animator.GetBool(isSuccessId))
        {
            // PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isSuccess", true);
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, isSuccessId, true);
            // PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isOn", true);
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, isOnId, true);
            EnvironmentManager.instance.TogglePower(true);
            // EnvironmentManager.instance.isPowerOn = true;
            // animator.SetBool("isSuccess", true);
            // animator.SetBool("isOn", true);
        }
        else if (!combi.Equals(correctCombi))
        {
            // PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isSuccess", false);
            // PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, "isOn", true);
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, isSuccessId, false);
            PV.RPC("RPC_HandlePowerGenerator", RpcTarget.All, isOnId, true);
            EnvironmentManager.instance.TogglePower(false);
            // EnvironmentManager.instance.isPowerOn = false;
            // animator.SetBool("isSuccess", false);
            // animator.SetBool("isOn", true);
            combi = "";
        }
    }

    [PunRPC]
    private void RPC_HandlePowerGenerator(int animationId, bool val)
    {
        animator.SetBool(animationId, val);
    }
}
