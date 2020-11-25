using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;
    public Animator animator;
    public PlayerController playerController;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            shootBow();
        }
    }

    void shootBow()
    {
        // arow logic
        Vector3 mouseWorld= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDirection = mouseWorld - transform.position;
        shootingDirection.Normalize();
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection;
        float rotation = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        arrow.transform.Rotate(0.0f,0.0f, rotation);

        // Play animation
        //
        //Debug.Log(rotation);
        if ((rotation < 30 && rotation >= 0) || (rotation <= 180 && rotation > 150) || (rotation > -30 && rotation < 0) || (rotation >= -180 && rotation < -150))
            animator.SetTrigger("BowHorizontal"); // horizontal 
        else if(rotation <= -30 && rotation >= -150)
            animator.SetTrigger("BowDown"); // Down
        else if (rotation >= 30 && rotation <= 150)
            animator.SetTrigger("BowUp"); // Up

        // Flip player to face fire direction
        //
        if ((Mathf.Abs(rotation) > 90 && playerController.checkIfFacingRight()) || (Mathf.Abs(rotation) <= 90 && !playerController.checkIfFacingRight()))
            playerController.Flip();

        // Play sound
        //
        FindObjectOfType<AudioManager>().Play("BowSound");
    }
}
