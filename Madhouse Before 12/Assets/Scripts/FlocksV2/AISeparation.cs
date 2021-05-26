using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeparation : MonoBehaviour
{
    GameObject[] enemies;
    public float SpaceBetween = 1.5f;
    private float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAllEnemies());
        // enemies = GameObject.FindGameObjectsWithTag("enemies");
    }

    IEnumerator GetAllEnemies()
    {
        yield return new WaitForSeconds(2f);
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                GameObject e = enemies[i];
                if (e != gameObject)
                {
                    float distance = Vector3.Distance(e.transform.position, this.transform.position);
                    if (distance < SpaceBetween)
                    {
                        Vector3 direction = transform.position - e.transform.position;
                        transform.Translate(direction * Time.deltaTime);
                    }
                }
            }
        }
    }
}
