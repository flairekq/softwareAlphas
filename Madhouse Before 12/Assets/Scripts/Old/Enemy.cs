using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to identify the monster as an enemy
// such that when the player interacts with it (be it mouse or key)
// the monster will be attacked by the player
// [RequireComponent(typeof(EnemyStats))]
// public class Enemy : Interactable
public class Enemy : MonoBehaviour
{
    // PlayerManagerOld playerManagerOld;
    EnemyStats myStats;
    EController2 enemyController;

    void Start()
    {
        // playerManagerOld = PlayerManagerOld.instance;
        myStats = GetComponent<EnemyStats>();
        enemyController = transform.parent.transform.parent.GetComponent<EController2>();
        // myStats = gameObject.transform.parent.transform.parent.GetComponent<EnemyStats>();
    }

    // public override void Interact()
    // {
    // base.Interact();
    // Attack the enemy
    // CharacterCombat playerCombat = playerManagerOld.player.GetComponent<CharacterCombat>();
    // if (playerCombat != null) {
    //     playerCombat.Attack(myStats);
    // }
    // }

    public void Attacked(CharacterCombat player, int pvID)
    {
        player.Attack(myStats);
        // Debug.Log("attacked enemy");
        enemyController.AttackedByPlayer(pvID);
    }
}
