using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNamePlate : MonoBehaviourPun
{
    [SerializeField] private Text usernameText;

    //[SerializeField] private PlayerMotor player;
    PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if(PV.IsMine)
        {
            return;
        }
        usernameText.text = PV.Owner.NickName;
    }
    /*
    // Update is called once per frame
    void Update()
    {
        usernameText.text = player.name;
    } */
}
