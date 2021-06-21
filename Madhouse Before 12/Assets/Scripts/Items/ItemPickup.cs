using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    public bool PickUp(Inventory inventory)
    {
        // Debug.Log("picking up " + item.name);
        bool wasPickedUp = inventory.Add(gameObject);
        if (wasPickedUp)
        {
            // Destroy(gameObject);
            // gameObject.SetActive(false);
            PV.RPC("RPC_HandleItem", RpcTarget.All, false, this.transform.position);
            return true;
        }
        return false;
    }

    public void MakeVisible(Vector3 pos)
    {
        PV.RPC("RPC_HandleItem", RpcTarget.All, true, pos);
    }

    [PunRPC]
    private void RPC_HandleItem(bool active, Vector3 pos)
    {
        GameObject item = PhotonView.Find(PV.ViewID).gameObject;
        item.SetActive(active);
        item.transform.position = pos;
    }
}
