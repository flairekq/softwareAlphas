using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EscMenu : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private PhotonView PV;
    [SerializeField] private GameObject menu;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider mouseSlider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float multipler = 30f;

    [SerializeField] private GameObject confirmDialog;

    private TogglePlayerCursor togglePlayerCursor;
    private bool isAlreadyCursorMode = false;
    [SerializeField] private bool isStartScene = false;
    private GameObject leaveGameObject;
    public bool isInEscMode = false;
    // private float bgmVol;
    // private float effectsVol;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        togglePlayerCursor = GetComponent<TogglePlayerCursor>();
        float bgmVol = PlayerPrefs.GetFloat("bgm", 1);
        float effectsVol = PlayerPrefs.GetFloat("effects", 1);
        float mouseSensitivity = PlayerPrefs.GetFloat("mouse", 3);
        bgmSlider.value = bgmVol;
        effectsSlider.value = effectsVol;
        mouseSlider.value = mouseSensitivity;
        leaveGameObject = GameObject.FindGameObjectWithTag("LeaveGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (!isStartScene && Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                if (!isAlreadyCursorMode)
                {
                    togglePlayerCursor.ChangeToPlayer();
                }
                menu.SetActive(false);
                isInEscMode = false;
            }
            else
            {
                isInEscMode = true;
                isAlreadyCursorMode = togglePlayerCursor.IsInCursorMode();
                togglePlayerCursor.ChangeToCursor(false);
                menu.SetActive(true);
            }
        }
    }

    public void OnSliderValueChange(int type)
    {
        if (type == 0)
        {
            PlayerPrefs.SetFloat("bgm", bgmSlider.value);
            if (bgmSlider.value > 0.001)
            {
                mixer.SetFloat("MusicVolume", Mathf.Log10(bgmSlider.value) * multipler);
            }
            else
            {
                mixer.SetFloat("MusicVolume", -80.0f);
            }
        }
        else if (type == 1)
        {
            PlayerPrefs.SetFloat("effects", effectsSlider.value);
            if (effectsSlider.value > 0.001)
            {
                mixer.SetFloat("SFXVolume", Mathf.Log10(effectsSlider.value) * multipler);
            }
            else
            {
                mixer.SetFloat("SFXVolume", -80.0f);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("mouse", mouseSlider.value);
        }
    }

    public void ToggleConfirmDialog(bool isShow)
    {
        confirmDialog.SetActive(isShow);
    }

    public void GoToMainMenu()
    {
        if (leaveGameObject != null)
        {
            leaveGameObject.GetComponent<LeaveGame>().GoToMainMenu();
        }
    }
}
