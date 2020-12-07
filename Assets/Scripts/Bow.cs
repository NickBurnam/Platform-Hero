using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;
    public Animator animator;
    public PlayerController playerController;
    public int maxAmmo = 10;
    private int currAmmo = 10;
    public Text ammoUI;
    private bool bUnlocked = false;
    // Update is called once per frame
    void Update()
    {
        ammoUI.text = currAmmo.ToString();
        if (bUnlocked && Input.GetButtonDown("Fire2") && currAmmo > 0)
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

        // Decrement currAmmo
        //
        currAmmo--;
    }

    public int getCurrAmmo()
    {
        return currAmmo;
    }

    public bool addAmmo(int amount)
    {
        if(currAmmo == maxAmmo)
        {
            return false;                       // unsuccesful ammo increase
        }
        else if(currAmmo + amount >= maxAmmo)   // dont increase ammo past max
        {
            currAmmo = maxAmmo;
            return true;                        // succesful ammo increase
        }
        else
        {
            currAmmo += amount;                 // add amount to currAmmo
            return true;                        // succesful ammo increase
        }
    }

    public void unlockBow()
    {
        if (!bUnlocked)
        {
            bUnlocked = true;
        }
    }
}
