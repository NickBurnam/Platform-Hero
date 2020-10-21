﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Rigidbody2D playerPhysics;
    public Animator animator;
    public int maxHP = 100;
    int currentHP;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHP(maxHP);
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
        Debug.Log("Player died!");

        // Death animation
        //
        animator.SetBool("IsDead", true);

        // Disable player
        //
        GetComponent<PlayerController>().enabled = false;
        //this.enabled = false;

        // Game Over
        //
        FindObjectOfType<GameManager>().EndGame();

    }
}
