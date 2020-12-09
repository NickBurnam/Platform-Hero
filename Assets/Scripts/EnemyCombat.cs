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
    public float playerRange = 2.0f;
    public int attackDamage = 25;
    public float attackRate = 2.0f;
    float nextAttackTime = 0f;

    private PlayerController player;
    public bool playerInRange = false;
    public bool isBoss = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(attackPoint.position, playerRange, enemyLayers);

        if (playerInRange && Time.time >= nextAttackTime)
        {
            // Play animation
            //
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            if (isBoss)
            {
                FindObjectOfType<AudioManager>().Play("DragonFireSound");
            }
            Invoke("Attack", 0.5f); // sync up attack with fire animation so player takes damage at the right time
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
