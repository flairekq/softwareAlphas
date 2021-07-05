using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyStats : CharacterStats
{
    [SerializeField] private Animator animator;
    // private void Start() {
    //     // animator = gameObject.transform.parent.GetComponent<Animator>();
    //     // animator = GetComponent<Animator>();
    // }
    private PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }


    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.T)) {
        //     TakeDamage(10);
        // }
        if (currentHealth <= 0 && PV.IsMine && PhotonNetwork.IsMasterClient)
        {
            Die();
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        // Debug.Log(transform.name + " takes " + damage + " damage.");

        // if (currentHealth <= 0)
        // {
        //     Die();
        // }
        // if (PV.IsMine)
        // {
        PV.RPC("RPC_HandleDamage", RpcTarget.All, damage);
        // }
    }

    [PunRPC]
    private void RPC_HandleDamage(int damage)
    {
        currentHealth -= damage;
    }

    public override void Die()
    {
        // base.Die();
        isDead = true;
        animator.SetBool("isDead", true);
    }
}
