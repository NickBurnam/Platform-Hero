using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;
    public HealthBar healthBar;
    public Transform position;
    public PlayerScore playerScore;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHP(maxHP);
    }

    void Update()
    {
        healthBar.transform.position = position.position + new Vector3(0,0.5f,0);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHP(currentHP);

        // Play hurting animation
        //

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        isDead = true;
        // Add to Player score
        //
        FindObjectOfType<GameManager>().AddScore(100);
       
        // Death animation
        //

        // Disable enemy
        //
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyPatrol>().enabled = false;
        GetComponent<EnemyCombat>().enabled = false;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
