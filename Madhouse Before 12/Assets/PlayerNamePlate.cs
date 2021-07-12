using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNamePlate : MonoBehaviour
{
    [SerializeField] private Text usernameText;
    [SerializeField] private PlayerMotor player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        usernameText.text = player.name;
    }
}
