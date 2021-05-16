using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    private float speed = 2.5f;
    public Transform player;
    private Vector3 enemyToPlayer;
    private bool isInBattleState = false;

    // Start is called before the first frame update
    void Start()
    {
        isInBattleState = false;
        // anim.SetInteger("battle", 1);
    }

    // Update is called once per frame
    void Update()
    {
        enemyToPlayer = player.transform.position - transform.position;
        var dist = enemyToPlayer.magnitude;
        if (dist >= 2f)
        {
            RunTowardsPlayer();
        }
        else
        {
            ToggleBattleState();
              if (isInBattleState) {
                Attack();
            }           
        }
    }

    void RunTowardsPlayer()
    {
        transform.rotation = Quaternion.LookRotation(enemyToPlayer);
        gameObject.GetComponent<CharacterController>().Move(enemyToPlayer.normalized * speed * Time.deltaTime);
        anim.SetInteger("moving", 16);
    }

    void ToggleBattleState() {
        // anim.SetInteger("moving", 0);
        if (isInBattleState) {
            anim.SetInteger("battle", 0);
            isInBattleState = false;
        } else {
            anim.SetInteger("battle", 1);
            isInBattleState = true;
        }
    }

    void Attack()
    {
        int n = Random.Range(0, 3);
        if (n == 0) {
            anim.SetInteger("moving", 2);
        }
        else if (n == 1) {
            anim.SetInteger("moving", 3);
        } else {
            anim.SetInteger("moving", 4);
        }
    }
}
