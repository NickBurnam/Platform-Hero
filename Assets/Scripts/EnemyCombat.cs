using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float damageDelay = 0.0f;

    public float attackRange = 0.5f;
    public int attackDamage = 25;
    public float attackRate = 2.0f;
    float nextAttackTime = 0f;

    private PlayerController player;
    public LayerMask playerLayer;
    public bool playerInRange = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        // Detect enemies in range of attack
        //
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Do damage
        //
        foreach (Collider2D enemy in hitEnemies)
        {
            // Play animation
            //
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            enemy.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
