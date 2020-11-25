using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlight : MonoBehaviour
{
    private PlayerController player;
    public LayerMask playerLayer;
    public bool playerInRange = false;
    public float speed = 2;
    public float playerRange = 10;
    public EnemyHealth enemyhealth;
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
        if (playerInRange && !enemyhealth.IsDead())
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (enemyhealth.IsDead())
        {
            rigidBody.gravityScale = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }
}
