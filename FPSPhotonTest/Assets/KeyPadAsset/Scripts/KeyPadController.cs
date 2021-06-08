//Script written by Matthew Rukas - Volumetric Games || volumetricgames@gmail.com || www.volumetric-games.com

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyPadController : MonoBehaviour
{
    [SerializeField] private string validCode;
    public int characterLim;
    public InputField codeText;

    // [SerializeField] private GameObject door;
    [SerializeField] private GameObject keypadModel;

    [SerializeField] private AudioClip beep;
    [SerializeField] private AudioClip denied;
    public Text text;
    private AudioSource mainAudio;

    void Start()
    {
        mainAudio = GetComponent<AudioSource>();
    }

    public void SingleBeep()
    {
        mainAudio.PlayOneShot(beep, 0.2f);
    }

    public void CheckCode()
    {
        if (codeText.text == validCode)
        {
            // door.GetComponent<OpenDoor>().open = true;
            // keypadModel.tag = "Untagged"; 
            Debug.Log("correct code");
            text.color =  new Color(25f/255f, 137f/255f, 8f/255f, 255f);;
        }

        else
        {
            mainAudio.PlayOneShot(denied, 0.2f);
            text.color = Color.red;
        }
    }
}
