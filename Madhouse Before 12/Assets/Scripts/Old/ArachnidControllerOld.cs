using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArachnidControllerOld : MonoBehaviour
{
    public Animator anim;
    private float speed = 2.5f;
    // public Transform player;
    private Vector3 enemyToPlayer;
    private CharacterController controller;

    private bool isDead = false;
    private bool isChasing = false;
    private int attack = 0;
    private float verticalVelocity;
    private float gravity = 9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        enemyToPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        var dist = enemyToPlayer.magnitude;
        if (dist >= 2f)
        {
            if (!isChasing)
            {
                isChasing = true;
                anim.SetBool("isChasing", isChasing);
            }
            RunTowardsPlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                anim.SetBool("isChasing", isChasing);
            }
            Attack();
        }
    }

    void RunTowardsPlayer()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        transform.rotation = Quaternion.LookRotation(enemyToPlayer);
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(enemyToPlayer.normalized * speed * Time.deltaTime);
        // controller.Move(transform.forward * speed * Time.deltaTime);
        controller.Move(moveVector  * Time.deltaTime);
    }

    void Attack()
    {
        int n = Random.Range(0, 2);
        if (n == 0)
        {
            anim.SetInteger("attack", 1);
        }
        else
        {
            anim.SetInteger("attack", 2);
        }
    }
}
