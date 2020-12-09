using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 1;
    private bool movingRight = true;
    private bool hitWall = false;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundDetection;

    private PlayerController player;
    public LayerMask playerLayer;
    public bool playerInRange = false;
    public float playerRange = 10;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            // Move towards player
            //
            Vector3 toPlayer = player.transform.position - transform.position;

            if (toPlayer.x > 0) // move right
            {
                if (movingRight) // keep moving right
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    movingRight = true;
                }
                else // moving left so flip
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
            else if (toPlayer.x < 0) // move left
            {
                if (!movingRight) // keep moving left
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    movingRight = false;
                }
                else // flip
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
            }
            else
            {
                // dont move
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        hitWall = Physics2D.OverlapCircle(groundDetection.position, checkRadius, groundLayer);

        // Handle movement on floating platform
        //
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        // Handle movement on platform with walls
        //
        if (hitWall)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }
}
