using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 25;
    public Rigidbody2D rb;
    public float lifespan = 3.0f;
    public LayerMask groundLayer;
    public LayerMask enemyLayers;
    public GameObject sharpPoint;
    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity *= speed;
    }

    void Update()
    {
        Destroy(gameObject, lifespan);
        // Detect enemies in range of attack
        //
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(sharpPoint.transform.position, 0.1f, enemyLayers);

        // Do damage
        //
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
