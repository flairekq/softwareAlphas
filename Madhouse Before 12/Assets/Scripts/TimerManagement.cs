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

    void Start()
    {
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
                if (updateTimerCooldown <= 0f) {
                    UpdateTimer();
                    updateTimerCooldown = 1f;
                } else {
                    updateTimerCooldown -= Time.deltaTime;
                }
            }
        }
    }

    void UpdateTimer()
    {
        timerIncrementValue = PhotonNetwork.Time - startTime;
        timeDisplay.text = TimeSpan.FromSeconds(elapsedTimeForDisplay + timerIncrementValue).ToString("hh':'mm':'ss");
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
                startTime = (double) propsTime;
                startTimer = true;
            }
        }
    }
}
