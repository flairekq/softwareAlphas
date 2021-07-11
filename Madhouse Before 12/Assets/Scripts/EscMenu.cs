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
                togglePlayerCursor.ChangeToPlayer();
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
            mixer.SetFloat("MusicVolume", Mathf.Log10(bgmSlider.value) * multipler);
        }
        else
        {
            PlayerPrefs.SetFloat("effects", effectsSlider.value);
            mixer.SetFloat("SFXVolume", Mathf.Log10(effectsSlider.value) * multipler);
        }
    }
}
