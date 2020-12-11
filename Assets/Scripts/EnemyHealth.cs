using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;
    public HealthBar healthBar;
    public Transform position;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public PlayerScore playerScore;
    private bool isDead = false;
    public bool isBoss = false;
    public GameObject endPortal;
    public GameObject bossHealth;
    public GameObject bossZone;

    private float nextRoar = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHP(maxHP);
    }

    void Update()
    {
        if (!isBoss)
        {
            healthBar.transform.position = position.position + offset;
        }
        else
        {
            if (Time.time >= nextRoar && !isDead)
            {
                DragonBreathe();
                nextRoar = Time.time + 30.0f;
            }
        }
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHP(currentHP);

        // Play hurting animation
        //

        if (currentHP <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void DisableBoss()
    {
        // enabled end portal
        //
        endPortal.SetActive(true);

        // disable boss zone
        //
        bossZone.SetActive(false);

        // disable health bar
        //
        bossHealth.SetActive(false);
    }

    private void DragonBreathe()
    {
        FindObjectOfType<AudioManager>().Play("DragonBreatheSound");
    }

    void Die()
    {
        if (isBoss && endPortal != null)
        {
            FindObjectOfType<AudioManager>().Play("DragonDeathSound"); 
            Invoke("DisableBoss", 3);
        }
        //Debug.Log("Enemy died!");

        // Add to Player score
        //
        FindObjectOfType<GameManager>().AddScore(100);
       
        // Death animation
        //

        // Disable enemy
        //
        GetComponent<Collider2D>().enabled = false;
        //GetComponent<EnemyPatrol>().enabled = false;
        GetComponent<EnemyCombat>().enabled = false;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
