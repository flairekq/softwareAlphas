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
    // [SerializeField] string startTime;
    // [SerializeField] float startTimeInSecs;
    // [SerializeField] float endTimeInSecs;
    // private float elapsedTime;
    // private PhotonView PV;
    // private bool timerGoing = false;
    // private TimeSpan timePlaying;

    void Awake()
    {
        instance = this;
    }

    // void Start()
    // {
    //     timeDisplay.text = startTime;
    //     BeginTimer();
    // }

    // public void BeginTimer()
    // {
    //     timerGoing = true;
    //     elapsedTime = startTimeInSecs;
    //     StartCoroutine(UpdateTimer());
    // }

    // public void EndTimer()
    // {
    //     timerGoing = false;
    // }

    // private void Update()
    // {
    //     if (elapsedTime >= endTimeInSecs)
    //     {
    //         EndTimer();
    //         Debug.Log("Time's up");
    //     }
    // }

    // private IEnumerator UpdateTimer()
    // {
    //     while (timerGoing)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         timePlaying = TimeSpan.FromSeconds(elapsedTime);
    //         timeDisplay.text = timePlaying.ToString("hh':'mm':'ss");
    //         yield return null;
    //     }
    // }

    bool startTimer = false;
    double timerIncrementValue;
    [SerializeField] double elapsedTimeForDisplay = 41400;
    double startTime;
    // in seconds
    [SerializeField] double timer = 20;

    ExitGames.Client.Photon.Hashtable CustomValue;

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
                Debug.Log("time up");
                GameController.instance.GameOver();
                this.enabled = false;
            }
            else
            {
                UpdateTimer();
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
