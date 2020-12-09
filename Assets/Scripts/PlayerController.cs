using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 2;
    public int jumpForce = 5;
    private int jumpCount;
    public int maxJumps = 2;
    public Rigidbody2D rb;
    private bool facingRight = true;

    [SerializeField]
    private bool isGrounded = true;

    public Transform groundCheckOrigin;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask itemLayer;

    public Animator animator;
    private bool bInputEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for ground collision for player
        isGrounded = Physics2D.OverlapCircle(groundCheckOrigin.position, checkRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        // Handle jump logic
        if (isGrounded)
        {
            
            if (bInputEnabled && Input.GetButtonDown("Jump"))
            {
                // Play animation
                //
                animator.SetBool("IsJumping", true);

                // Play sound
                //
                FindObjectOfType<AudioManager>().Play("PlayerJump1");

                rb.velocity = Vector2.up * jumpForce;
                
                jumpCount = 1;
            }
            else
            {
                // Don't play animation
                //
                animator.SetBool("IsDoubleJumping", false);

                jumpCount = 0;
            }
        }
        else if (bInputEnabled && !isGrounded && Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            // Play animation
            //
            animator.SetBool("IsDoubleJumping", true);

            // Play sound
            //
            FindObjectOfType<AudioManager>().Play("PlayerJump2");

            rb.velocity = Vector2.up * jumpForce;
            jumpCount = 2;
        }

        // Fall too far down
        //
        if(rb.position.y < -30f)
        {
            FindObjectOfType<PlayerHealth>().Die();
        }
    }

    // Update once per physics frame
    private void FixedUpdate()
    {
        if (bInputEnabled)
        {
            float x_dir = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(x_dir * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(x_dir));


            if (facingRight && x_dir < 0)
            {
                Flip();
            }
            else if (facingRight == false && x_dir > 0)
            {
                Flip();
            }
        }
        
    }

    // Flip sprite
    public void Flip()
    {
        facingRight = !facingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        //transform.Rotate(0f, 180f, 0f);
    }

    public bool checkIfFacingRight()
    {
        return facingRight;
    }

    public void DisableInput()
    {
        bInputEnabled = false;
    }
}
