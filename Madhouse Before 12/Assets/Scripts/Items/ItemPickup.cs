using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ItemPickup : MonoBehaviourPunCallbacks
{
    private Item item;
    private PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        item = gameObject.GetComponent<Item>();
    }

    public void PickUp(Inventory inventory)
    {
        // Debug.Log("picking up " + item.name);
        bool wasPickedUp = inventory.Add(gameObject);
        if (wasPickedUp)
        {
            // Destroy(gameObject);
            // gameObject.SetActive(false);
            PV.RPC("RPC_HandleItem", RpcTarget.AllBuffered, false);
        }
    }

    public void MakeVisible()
    {
        PV.RPC("RPC_HandleItem", RpcTarget.AllBuffered, true);
    }

    [PunRPC]
    private void RPC_HandleItem(bool active)
    {
        GameObject item = PhotonView.Find(PV.ViewID).gameObject;
        item.SetActive(active);
    }
}
