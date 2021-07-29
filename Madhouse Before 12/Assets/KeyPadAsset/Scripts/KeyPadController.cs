﻿//Script written by Matthew Rukas - Volumetric Games || volumetricgames@gmail.com || www.volumetric-games.com

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyPadController : MonoBehaviour
{
    [SerializeField] private string validCode;
    public int characterLim;
    public InputField codeText;
    [SerializeField] private GameObject keypadModel;
    [SerializeField] private string roomName;
    [SerializeField] private AudioClip beep;
    [SerializeField] private AudioClip denied;
    [SerializeField] Image[] characterCheckers;
    [SerializeField] Animator animator;
    public Text text;
    private AudioSource mainAudio;
    private int isWrongId;

    void Start()
    {
        mainAudio = GetComponent<AudioSource>();
        isWrongId = Animator.StringToHash("isWrong");
    }

    public void SingleBeep()
    {
        mainAudio.PlayOneShot(beep, 0.2f);
    }

    public void CheckCode()
    {
        if (codeText.text == validCode)
        {
            // Debug.Log("correct code");
            // text.color = new Color(25f / 255f, 137f / 255f, 8f / 255f, 255f);
            foreach (Image i in characterCheckers)
            {
                i.color = new Color(0f / 255f, 157f / 255f, 11f / 255f);
            }
            EnvironmentManager.instance.ToggleLockUnlockDoor(roomName, true);
        }
        else
        {
            // Color checkerColor = new Color();
            for (int i = 0; i < codeText.text.Length; i++)
            {
                if (codeText.text[i].Equals(validCode[i]))
                {
                    characterCheckers[i].color = new Color(0f / 255f, 157f / 255f, 11f / 255f);
                    // ColorUtility.TryParseHtmlString("#009d0b", out checkerColor);
                }
                else
                {
                    characterCheckers[i].color = new Color(196f / 255f, 12f / 255f, 1f / 255f);
                    // ColorUtility.TryParseHtmlString("#C40C01", out checkerColor);
                }
            }
            if (codeText.text.Length < 4)
            {
                for (int i = codeText.text.Length - 1; i < 4; i++)
                {
                    characterCheckers[i].color = new Color(196f / 255f, 12f / 255f, 1f / 255f);
                }
            }
            animator.SetBool(isWrongId, true);
            StartCoroutine(OffAnimation());
            mainAudio.PlayOneShot(denied, 0.2f);
            // text.color = Color.red;
            EnvironmentManager.instance.ToggleLockUnlockDoor(roomName, false);
            TimerManagement.instance.AdjustTime(60, false);
        }
    }

    IEnumerator OffAnimation() {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(isWrongId, false);
    }
}
