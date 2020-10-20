using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // Play hurting animation
        //

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        // Death animation
        //

        // Disable enemy
        //
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
