using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DiaryClue : MonoBehaviour
{
    public string word = "";
    public bool isPickedBefore = false;
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Picked()
    {
        PV.RPC("RPC_HandleDiaryClue", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_HandleDiaryClue()
    {
        isPickedBefore = true;
    }
}
