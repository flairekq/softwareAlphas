using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EscMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    [SerializeField] private GameObject menu;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectsSlider;
    // [SerializeField] private string volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float multipler = 30f;

    private TogglePlayerCursor togglePlayerCursor;

    // private float bgmVol;
    // private float effectsVol;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        togglePlayerCursor = GetComponent<TogglePlayerCursor>();
        float bgmVol = PlayerPrefs.GetFloat("bgm", 1);
        float effectsVol = PlayerPrefs.GetFloat("effects", 1);
        bgmSlider.value = bgmVol;
        effectsSlider.value = effectsVol;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                if (!togglePlayerCursor.IsInCursorMode())
                {
                    togglePlayerCursor.ChangeToPlayer();
                }
                menu.SetActive(false);
            }
            else
            {
                togglePlayerCursor.ChangeToCursor();
                menu.SetActive(true);
            }
        }
    }

    public void OnSoundSliderValueChange(int type)
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
        else
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
    }
}
