using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGoal : MonoBehaviour
{
    protected Transform target;
    public float SpaceBetween = 1.5f;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) > SpaceBetween)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction * Time.deltaTime);
        }
    }
}
