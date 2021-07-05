using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    // public int currentHealth { get; private set; }
    public int currentHealth;
    public int damage;
    public bool isDead = false;
    private PhotonView PV;
    // [SerializeField] private string type = "E";

    void Awake()
    {
        currentHealth = maxHealth;
        // if (type.Equals("E"))
        // {
        //     PV = gameObject.transform.parent.transform.parent.GetComponent<PhotonView>();
        // }
        PV = GetComponent<PhotonView>();
    }


    // void Update()
    // {
    //     // if (Input.GetKeyDown(KeyCode.T)) {
    //     //     TakeDamage(10);
    //     // }
    //     if (currentHealth <= 0 && PV.IsMine && PhotonNetwork.IsMasterClient)
    //     {
    //         Die();
    //     }
    // }

    // [PunRPC]
    // public void TakeDamage(int damage)
    // {
    //     // Debug.Log(transform.name + " takes " + damage + " damage.");

    //     // if (currentHealth <= 0)
    //     // {
    //     //     Die();
    //     // }
    //     if (PV.IsMine)
    //     {
    //         PV.RPC("RPC_HandleDamage", RpcTarget.All, damage);
    //     }
    // }

    // [PunRPC]
    // private void RPC_HandleDamage(int damage)
    // {
    //     currentHealth -= damage;
    // }

    public virtual void Die()
    {
        // Die in some way 
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
