using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TimerManagement : MonoBehaviour
{
    public static TimerManagement instance;
    [SerializeField] Text timeDisplay;

    void Awake()
    {
        instance = this;
    }

    bool startTimer = false;
    double timerIncrementValue;
    [SerializeField] double elapsedTimeForDisplay = 41400;
    double startTime;
    // in seconds
    [SerializeField] double timer = 20;

    ExitGames.Client.Photon.Hashtable CustomValue;
    private float updateTimerCooldown = 1f;

    private PhotonView PV;
    [SerializeField] private int monsterDeathAddTimeCounter = 20;
    private float displayAdjustTimingCounter = 2f;
    private bool isDisplayAdjustTiming = false;
    [SerializeField] Text adjustTimingText;

    void Start()
    {
        instance = this;
        instance.PV = GetComponent<PhotonView>();
        SetStartTimer();
    }

    void Update()
    {
        if (!startTimer)
        {
            SetStartTimer();
        }
        else
        {
            if (timerIncrementValue >= timer)
            {
                //Timer Completed
                //Do What Ever You What to Do Here
                // Debug.Log("time up");
                GameController.instance.GameOver(true);
                this.enabled = false;
            }
            else
            {
                if (updateTimerCooldown <= 0f)
                {
                    UpdateTimer();
                    updateTimerCooldown = 1f;
                }
                else
                {
                    updateTimerCooldown -= Time.deltaTime;
                }

                if (TimerManagement.instance.isDisplayAdjustTiming)
                {
                    if (TimerManagement.instance.displayAdjustTimingCounter <= 0f)
                    {
                        TimerManagement.instance.adjustTimingText.enabled = false;
                        TimerManagement.instance.displayAdjustTimingCounter = 2f;
                    }
                    else
                    {
                        TimerManagement.instance.displayAdjustTimingCounter -= Time.deltaTime;
                    }
                }
            }
        }
    }

    void UpdateTimer()
    {
        timerIncrementValue = PhotonNetwork.Time - startTime;
        timeDisplay.text = TimeSpan.FromSeconds(TimerManagement.instance.elapsedTimeForDisplay + timerIncrementValue).ToString("hh':'mm':'ss");
    }

    void SetStartTimer()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            CustomValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomValue);
        }
        else
        {
            object propsTime;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("StartTime", out propsTime))
            {
                startTime = (double)propsTime;
                startTimer = true;
            }
        }
    }

    public void AdjustTime(double time, bool isKillMonster)
    {
        TimerManagement.instance.PV.RPC("RPC_HandleAdjustTime", RpcTarget.All, time, isKillMonster);
    }

    [PunRPC]
    private void RPC_HandleAdjustTime(double time, bool isKillMonster)
    {
        if (isKillMonster)
        {
            if (TimerManagement.instance.monsterDeathAddTimeCounter > 0)
            {
                TimerManagement.instance.monsterDeathAddTimeCounter -= 1;
                TimerManagement.instance.elapsedTimeForDisplay += time;
                // TimerManagement.instance.displayAdjustTimingCounter = 2f;
                TimerManagement.instance.adjustTimingText.text = "-30s";
                TimerManagement.instance.adjustTimingText.enabled = true;
                TimerManagement.instance.isDisplayAdjustTiming = true;
            }
        }
        else
        {
            TimerManagement.instance.elapsedTimeForDisplay += time;
            // TimerManagement.instance.displayAdjustTimingCounter = 2f;
            TimerManagement.instance.adjustTimingText.text = "+" + time.ToString() + "s";
            TimerManagement.instance.adjustTimingText.enabled = true;
            TimerManagement.instance.isDisplayAdjustTiming = true;
        }
    }
}
