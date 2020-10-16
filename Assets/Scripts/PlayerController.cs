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

    public Animator animator;

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

        // Handle jump logic
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpCount = 1;
            }
            else
            {
                jumpCount = 0;
            }
        }
        else if (!isGrounded && Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpCount = 2;
        }
    }

    // Update once per physics frame
    private void FixedUpdate()
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

    // Flip sprite
    private void Flip()
    {
        facingRight = !facingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
