using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public Bow bow;
    public int ammoBonus = 10;

    private void Awake()
    {
        bow = FindObjectOfType<Bow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bow.addAmmo(ammoBonus))
        {
            FindObjectOfType<AudioManager>().Play("PickupSound");
            Destroy(gameObject);
        }
    }
}
