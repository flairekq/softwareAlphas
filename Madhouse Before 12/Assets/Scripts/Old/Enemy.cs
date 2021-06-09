using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to identify the monster as an enemy
// such that when the player interacts with it (be it mouse or key)
// the monster will be attacked by the player
[RequireComponent(typeof(EnemyStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    EnemyStats myStats;

    void Start() {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<EnemyStats>();
    }

    public override void Interact()
    {
        // base.Interact();
        // Attack the enemy
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null) {
            playerCombat.Attack(myStats);
        }
    }
}
