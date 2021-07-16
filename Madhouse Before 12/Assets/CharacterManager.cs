using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;



public class CharacterManager : MonoBehaviourPun
{
    public GameObject[] Characters;

    public int selectedCharacter = 0;
    
    public void NextCharacter()
    {
        Characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter +1) 
        % Characters.Length;
        Characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        Characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += Characters.Length;
        }
        Characters[selectedCharacter].SetActive(true);
    }

    
}
