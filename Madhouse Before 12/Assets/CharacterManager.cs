using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;



public class CharacterManager : MonoBehaviourPun
{
    public GameObject[] Characters;

    public int selectedCharacter = 0;
    
    public void ViewMaleAvatar()
    {
        selectedCharacter = 0;
        Characters[1].SetActive(false);
        Characters[0].SetActive(true);
    }

    public void ViewFemaleAvatar()
    {
        selectedCharacter = 1;
        Characters[0].SetActive(false);
        Characters[1].SetActive(true);
    }

    public void startingAvatar()
    {
         Characters[0].SetActive(true);
         Characters[1].SetActive(false);
    }
    public void selectAvatar()
    {
         Characters[0].SetActive(false);
         Characters[1].SetActive(false);
    }
    
    
    public int chooseAvatar()
    {
        return selectedCharacter;

    }

    
}
