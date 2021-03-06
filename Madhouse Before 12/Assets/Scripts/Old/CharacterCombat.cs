using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    // public float attackSpeed = 1f;
    // private float attackCooldown = 0f;
    // public float attackDelay = 0.6f;
    // public event System.Action OnAttack;
    CharacterStats myStats;
    [SerializeField] private string type = "E";

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        // if (type.Equals("P"))
        // {
        //     myStats = GetComponent<CharacterStats>();
        // }
        // else
        // {
        //     myStats = gameObject.transform.parent.transform.parent.GetComponent<CharacterStats>();
        // }
    }

    // void Update() {
    //     attackCooldown -= Time.deltaTime;
    // }

    public void Attack(EnemyStats targetStats)
    {
        // if (attackCooldown <= 0f) {
        //     StartCoroutine(DoDamage(targetStats, attackDelay));
        //     if (OnAttack != null) {
        //         OnAttack();
        //     }
        //     attackCooldown = 1f / attackSpeed;
        // }
        // StartCoroutine(DoDamage(targetStats, animationLength));
        // if (attackCooldown <= 0f) {
        //     targetStats.TakeDamage(myStats.damage.GetValue());
        //     // StartCoroutine(DoDamage(targetStats, animationLength));
        //     attackCooldown = 1f / attackSpeed;
        // }
        targetStats.TakeDamage(myStats.damage);
    }

    // IEnumerator DoDamage(CharacterStats stats, float delay) {
    //     yield return new WaitForSeconds(delay);
    //     stats.TakeDamage(myStats.damage.GetValue());
    // }
}
