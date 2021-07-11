using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Name : MonoBehaviour
{

    public Text nameText;

    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PV.IsMine)
        {
            nameText.text = PV.Owner.NickName;
        }
    }

}
